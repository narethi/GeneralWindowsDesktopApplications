using System;

namespace LogParserAndReader.Exceptions
{
    /// <summary>
    /// This error denotes a log property that could not be parsed into the template value
    /// </summary>
    internal sealed class TemplateValueTypeMismatchException : Exception
    {
    }
}
