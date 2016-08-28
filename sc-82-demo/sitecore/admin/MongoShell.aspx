<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="MongoShell.aspx.cs"
    Inherits="sc_82_demo.sitecore.admin.MongoShell" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->

    <title>Mongo Shell Tester - BETA</title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        .page-header {
            padding-bottom: 9px;
            margin: 62px 0 21px;
            border-bottom: 1px solid #eee;
        }
    </style>
</head>
<body>
    <form id="frmMongoShell" runat="server">

        <nav class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">

                    <a class="navbar-brand" href="#">Mongo Shell&nbsp;<sup><span class="label label-warning">BETA</span></sup></a>
                </div>

                <!--/.nav-collapse -->
            </div>
        </nav>

        <div class="container">


            <div class="page-header">
                <p class="lead">Mongo Shell - Helps you to validate your mongo connection, and do basic mongo checks!</p>
            </div>

            <div class="row">
                <div class="col-md-3">

                    <label class="control-label">Mongo DB :</label>
                    <asp:DropDownList runat="server" ID="ddlMongoDBs" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Operation :</label>

                    <asp:DropDownList runat="server" ID="ddlOperations" CssClass="form-control">
                        <asp:ListItem Text="Test Connection" Value="TC" />
                        <asp:ListItem Text="Find All Users" Value="FAC" />
                        <asp:ListItem Text="Get Collection Names" Value="GCN" />
                        <asp:ListItem Text="Get Collection" Value="GC" />
                        <asp:ListItem Text="Get Stats" Value="GS" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">

                    <label class="control-label">Input :</label>

                    <asp:TextBox ID="txtInput" runat="server" CssClass="form-control" placeholder="Text input" />

                </div>
                <div class="col-md-3">
                    <br />

                    <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Execute" CssClass="btn btn-primary" />
                </div>
            </div>
            <div>
                <p>&nbsp;</p>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Output : </h3>
                </div>
                <div class="panel-body">
                    <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" Rows="15" Columns="140" class="form-control" />

                </div>
            </div>

        </div>
        <!-- /.container -->

    </form>




    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax;/libs/jquery/1.12.4/jquery.min.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
</body>
</html>
