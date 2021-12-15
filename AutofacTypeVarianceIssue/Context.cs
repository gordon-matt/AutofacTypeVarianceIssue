using Autofac;

namespace AutofacTypeVarianceIssue
{
    internal static class Context
    {
        public static IContainer Container { get; set; } // So we can call from anywhere, if needed
    }
}