using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommonMark.Core;
using Main.Web.Features.FileImport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Web.Features.FileImport
{
    public class FileImportController : Controller
    {
        [Route("Upload")]
        public IActionResult Import()
        {
            return View();
        }


        [HttpPost]
        [Route("Upload")]
        public IActionResult Import(ICollection<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();

                for (int i = 0; i < files.Count; i++)
                {
                    var file = files.ElementAt(i);
                    if (file.Length > 0)
                    {
                        var content = ReadAsList(file);
                        for (int j = 0; j < content.Count; j++)
                        {
                            if (dictionary.ContainsKey(content[j][0]))
                            {
                                string[] value = dictionary[content[j][0]];
                                value[i] = content[j][1];
                            }
                            else
                            {
                                string[] newValue = new string[files.Count];
                                newValue[i] = content[j][1];
                                dictionary.Add(content[j][0], newValue);
                            }
                        }
                    }
                }
                return View("Preview", ConvertDictionaryToList(dictionary));
            }
        }

        [Route("Markdown")]
        public IActionResult Markdown()
        {
            return View(new MarkdownModel());
        }


        [HttpPost]
        [Route("Markdown")]
        public IActionResult Markdown(MarkdownModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                model.MarkdownHtml = CommonMarkConverter.Convert(model.MarkdownText);
                return View(model);
            }
        }


        public IActionResult Preview()
        {
            return View();
        }

        public static List<string[]> ReadAsList(IFormFile file)
        {
            var result = new List<string[]>();
            Regex lineSplitter = new Regex("(\"([^\"]*)\"|[^,]*)(,|$)");
            int? cols = null;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();


                    var columns = lineSplitter.Split(line)
                        .Where(s => s != String.Empty && s != "," && (s[0] != '"' && s[s.Length - 1] != '"'))
                        .ToArray();
                    if (!cols.HasValue)
                    {
                        cols = columns.Length;
                    }
                    string[] fixedSizedArray = new string[cols.Value];
                    for (int i = 0; i < fixedSizedArray.Length; i++)
                    {
                        if (fixedSizedArray[i] == null)
                        {
                            fixedSizedArray[i] = string.Empty;
                        }
                    }
                    Array.Copy(columns, fixedSizedArray, Math.Min(cols.Value, columns.Length));
                    if (!fixedSizedArray.Any(string.IsNullOrEmpty))
                    {
                        result.Add(fixedSizedArray);
                    }
                }
            }
            return result;
        }

        public static List<string[]> ConvertDictionaryToList(Dictionary<string, string[]> dictionary)
        {
            return dictionary.OrderBy(x => GetOrdering(x.Key)).Select(x => PrependToArray(x.Key, x.Value)).ToList();
        }

        public static int GetOrdering(string key)
        {
            //(x.Key=="Source Term")? 0 : x.Key.Length
            if (key == "Source Term")
            {
                return 0;
            }
            if (key.Length == 1)
            {
                return 5;
            }
            var isAlphaNumeric = key.All(char.IsLetterOrDigit);
            var hasWhiteSpace = key.Any(Char.IsWhiteSpace);
            if (isAlphaNumeric)
            {
                return 2;
            }
            if (hasWhiteSpace)
            {
                return 3;
            }
            return 4;
        }

        public static string[] PrependToArray(string value, string[] array)
        {
            string[] extendedByteArray2 = new string[array.Length + 1];
            extendedByteArray2[0] = value;
            Array.Copy(array, 0, extendedByteArray2, 1, array.Length);
            return extendedByteArray2;
        }
    }
}