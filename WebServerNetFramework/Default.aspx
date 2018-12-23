<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebServerNetFramework._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <table>
            <tr><asp:Button runat="server" id="btnVolUp" Text="Vol Up" OnClick="volUp_Click" style="width:30%;height:100px" /></tr>
            <tr><asp:Button runat="server" id="btnVolDown" Text="Vol Down" OnClick="volDown_Click" style="width:30%;height:100px"/></tr>
        </table>
            <br />
        <table>
            <tr><asp:Button runat="server" id="btnLeftKey" Text="Left" OnClick="leftKey_Click" style="width:30%;height:100px"/></tr>
            <tr><asp:Button runat="server" id="btnRightKey" Text="Right" OnClick="rightKey_Click" style="width:30%;height:100px"/></tr>
        </table>
        <br />
        <asp:Button runat="server" id="btnPausePlay" Text="Pause / Play" OnClick="space_Click" style="width:30%;height:100px;align-self:center"/>
        <br /><br /><br />
        <asp:Button runat="server" id="btnShutDown" Text="Shut Down" OnClick="shutdown_Click"/>
        </ContentTemplate>

    </asp:UpdatePanel>

    </div> 

          
</asp:Content>
