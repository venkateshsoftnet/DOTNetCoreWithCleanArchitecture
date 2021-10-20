namespace TFLAssessment.Console
{
    /// <summary>
    /// Class used to return exit code
    /// </summary>
    public static class ExitCodes
    {
        public static int Success => 0;
        public static int NotFound => 1;
        public static int BadInput => 2;
        public static int Failure => 3;
    }
}
