using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Main.Core.Extensions
{
    public static class SeparatedValuesExtension
    {
        public static void WriteTabSeparatedValues<T>(this TextWriter output, IEnumerable<T> data)
        {
            WriteSeparatedValues(output, data, '\t');
        }

        public static void WriteSeparatedValues<T>(TextWriter output, IEnumerable<T> data, char separator)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write(separator);
            }
            output.WriteLine();
            foreach (T item in data)
            {
                if (item != null)
                {
                    foreach (PropertyDescriptor prop in props)
                    {
                        var propValue = prop.GetValue(item);

                        if (prop.Converter != null && propValue != null)
                        {
                            string outputValue = prop.Converter.ConvertToString(propValue);
                            if (outputValue != null)
                            {
                                if (outputValue.Contains('"'))
                                {
                                    outputValue = outputValue.Replace("\"", "\"\"");
                                }
                                if (outputValue.Contains(separator) || outputValue.Contains('\n'))
                                {
                                    outputValue = string.Format("\"{0}\"", outputValue);
                                }
                                output.Write(outputValue);
                            }
                        }
                        output.Write(separator);
                    }
                    output.WriteLine();
                }
            }
        }
    }
}
