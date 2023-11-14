<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication5.Default" %>

<%@ Register Src="~/UserControls/TalentCard.ascx" TagName="TalentCard" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/TalentList.ascx" TagName="TalentList" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/TalentManagement.ascx" TagName="TalentManagement" TagPrefix="uc" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Talents Agancy</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="Scripts/Requests.js"></script>
    <link href="Styles/AgancyStyle.css" rel="stylesheet" />
</head>
<body>
    <h1>Talents Agancy</h1>
    <form id="form1" runat="server">
        <div>
            <uc:TalentList runat="server" ID="talentList" />
        </div>
        <div id="talentCardDiv" style="display: none">
            <uc:TalentCard runat="server" ID="talentCard" />
        </div>
        <div id="talentManagementDiv" style="display: none">
            <uc:TalentManagement runat="server" ID="talentManagement" />
        </div>
    </form>


</body>
</html>
