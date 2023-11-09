<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication5.Default" %>

<%@ Register Src="~/UserControls/TalentCard.ascx" TagName="TalentCard" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/TalentList.ascx" TagName="TalentList" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/TalentManagement.ascx" TagName="TalentManagement" TagPrefix="uc" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/Requests.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:TalentList runat="server" ID="talentList" />
        </div>
        <div id="talentCardDiv" style="display: none;">
            <uc:TalentCard runat="server" ID="talentCard" />
        </div>
        <div>
            <uc:TalentManagement runat="server" ID="talentManagement" />
        </div>
        <button id="try" onclick="doSomething()"></button>
    </form>


</body>
</html>
