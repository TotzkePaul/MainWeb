﻿namespace CommonMark.Core.Parser
{
    /// <summary>
    /// Class containing methods for fast forward matching of string contents
    /// </summary>
    internal static class ScannerCharacterMatcher
    {
        /// <summary>
        /// Moves along the given string as long as the current character is a whitespace.
        /// </summary>
        internal static bool MatchWhitespaces(string data, ref char currentCharacter, ref int currentPosition, int lastPosition)
        {
            var matched = false;
            while (Utilities.IsWhitespace(currentCharacter) && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        /// <summary>
        /// Moves along the given string as long as the current character is a ASCII letter.
        /// </summary>
        internal static bool MatchAsciiLetter(string data, ref char currentCharacter, ref int currentPosition, int lastPosition)
        {
            var matched = false;
            while (((currentCharacter >= 'a' && currentCharacter <= 'z')
                    || (currentCharacter >= 'A' && currentCharacter <= 'Z'))
                  && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        /// <summary>
        /// Moves along the given string as long as the current character is a valid HTML tag character 
        /// (ASCII letter or digit or dash).
        /// </summary>
        internal static bool MatchHtmlTagNameCharacter(string data, ref char currentCharacter, ref int currentPosition, int lastPosition)
        {
            var matched = false;
            while ((   (currentCharacter >= 'a' && currentCharacter <= 'z') 
                    || (currentCharacter >= 'A' && currentCharacter <= 'Z') 
                    || (currentCharacter >= '0' && currentCharacter <= '9')
                    || (currentCharacter == '-'))
                  && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        /// <summary>
        /// Moves along the given string as long as the current character is a ASCII letter or digit or one of the given additional characters.
        /// </summary>
        internal static bool MatchAsciiLetterOrDigit(string data, ref char currentCharacter, ref int currentPosition, int lastPosition, char valid1, char valid2, char valid3, char valid4)
        {
            var matched = false;
            while ((   currentCharacter == valid1
                    || currentCharacter == valid2
                    || currentCharacter == valid3
                    || currentCharacter == valid4
                    || (currentCharacter >= 'a' && currentCharacter <= 'z')
                    || (currentCharacter >= 'A' && currentCharacter <= 'Z')
                    || (currentCharacter >= '0' && currentCharacter <= '9'))
                  && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        /// <summary>
        /// Moves along the given string as long as the current character is a ASCII letter or digit or one of the given additional characters.
        /// </summary>
        internal static bool MatchAsciiLetterOrDigit(string data, ref char currentCharacter, ref int currentPosition, int lastPosition, char valid1)
        {
            var matched = false;
            while ((currentCharacter == valid1
                    || (currentCharacter >= 'a' && currentCharacter <= 'z')
                    || (currentCharacter >= 'A' && currentCharacter <= 'Z')
                    || (currentCharacter >= '0' && currentCharacter <= '9'))
                  && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        /// <summary>
        /// Moves along the given string as long as the current character is a ASCII letter or one of the given additional characters.
        /// </summary>
        internal static bool MatchAsciiLetter(string data, ref char currentCharacter, ref int currentPosition, int lastPosition, char valid1, char valid2)
        {
            var matched = false;
            while ((   currentCharacter == valid1 
                    || currentCharacter == valid2
                    || (currentCharacter >= 'a' && currentCharacter <= 'z')
                    || (currentCharacter >= 'A' && currentCharacter <= 'Z')
                    || (currentCharacter >= '0' && currentCharacter <= '9'))
                  && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        internal static bool MatchAnythingExcept(string data, ref char currentCharacter, ref int currentPosition, int lastPosition, char invalid1)
        {
            var matched = false;
            while (currentCharacter != invalid1 && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }

        internal static bool MatchAnythingExceptWhitespaces(string data, ref char currentCharacter, ref int currentPosition, int lastPosition,
            char invalid1, char invalid2, char invalid3, char invalid4, char invalid5, char invalid6)
        {
            var matched = false;
            while (currentCharacter != invalid1
                && currentCharacter != invalid2
                && currentCharacter != invalid3
                && currentCharacter != invalid4
                && currentCharacter != invalid5
                && currentCharacter != invalid6
                && (currentCharacter != ' ' && currentCharacter != '\n')
                && currentPosition < lastPosition)
            {
                currentCharacter = data[++currentPosition];
                matched = true;
            }
            return matched;
        }
    }
}
