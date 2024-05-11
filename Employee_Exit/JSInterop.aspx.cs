using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_Exit
{
    public partial class JSInterop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [Microsoft.JSInterop.JSInvokable]
        public string MyCSharpMethod(string input)
        {
            // Your C# logic here
            return "Hello from C#: " + input;
        }
    }
}