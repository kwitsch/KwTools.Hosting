using System.Diagnostics;

namespace KwTools.Hosting.TestExtension
{
    public class ExternalTestClass
    {
        public void WriteTypeName()
        {
            Debug.WriteLine(GetType().Name);
        }
    }
}
