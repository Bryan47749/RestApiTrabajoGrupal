<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApi.Default" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consumir un servicio Web API con C#</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">




        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }



        function ShowMessage(message, messagetype, reload) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(3000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 50);
        }
    </script>
</head>
<body>
    <div class="container">
        <!-- La imagen se configura en el archivo site.css -->
        <header class="jumbotron">
            <img src="Images/banner.jpg" />

        </header>
        <main>
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div tabindex="-1">
                            <div class="messagealert" id="alert_container"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 table-responsive">
                                <h1>Editar Categorias</h1>
                                <asp:GridView ID="gvCategories" runat="server"
                                    CssClass="table table-bordered table-condensed tablestriped"
                                    OnPreRender="gvCategories_PreRender" OnSelectedIndexChanged="gvCategories_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <HeaderStyle CssClass="bg-halloween" />
                                </asp:GridView>

                            </div>
                            <div id="details" class="col-sm-6">
                                <input type="hidden" id="orig_id" />
                                <div class="form-group">
                                    <label class="control-label">ID</label>
                                    <asp:TextBox ID="txtId" class="form-control" runat="server" onKeyPress="return soloNumeros(event)" MaxLength="5"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Nombre Corto </label>
                                    <asp:TextBox ID="txtShort" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Nombre Largo </label>
                                    <asp:TextBox ID="txtLong" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnInsertar" runat="server" CssClass="btn" Text="Insertar" OnClick="btnInsertar_Click" />
                                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn" Text="Actualizar" OnClick="btnActualizar_Click" />
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn" Text="Eliminar" OnClientClick="return confirm('Desea borrar!');" OnClick="btnEliminar_Click" />
                                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn" Text="Limpiar" OnClick="btnLimpiar_Click" />

                                </div>
                            </div>
                        </div>
                        <!-- fin de la fila (Row) -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </main>
    </div>
</body>
</html>

