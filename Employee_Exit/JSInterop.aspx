<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSInterop.aspx.cs" Inherits="Employee_Exit.JSInterop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JSInterop</title>
    <script>
        function invokeCSharpMethodFromJavaScript() {
            // Replace "assemblyName" and "YourNamespace.ExampleComponent.MyCSharpMethod" with your actual values
            DotNet.invokeMethodAsync('Employee_Exit', 'Employee_Exit.JSInterop.MyCSharpMethod', 'World')
                .then(result => {
                    // Handle the result returned from the C# method
                    console.log(result);

                    // Example: Display the result in an alert
                    alert(result);
                })
                .catch(error => {
                    // Handle any errors that occur during the invocation
                    console.error(error);
                });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
            <button type="button" onclick="invokeCSharpMethodFromJavaScript">Invoke C# Method from JavaScript</button>
        </div>
        </div>
    </form>
</body>
</html>
