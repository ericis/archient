namespace Razor.Templates
{
    using Archient.Razor;
    using System.Threading.Tasks;

    public class RazorTemplate : Archient.Razor.SimpleStringTemplate
    {
        #line hidden
        public RazorTemplate()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
            WriteLiteral("Hello world! 1 + 1 = ");
            Write(
#line 1 ""
                       1+1

#line default
#line hidden
            );

        }
        #pragma warning restore 1998
    }
}

