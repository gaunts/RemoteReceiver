<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebServerNetFramework._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative;">
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Button runat="server" id="btnVolUp" Text="Vol Up" OnClick="VolUp_Click" style="width:120px;height:100px;margin-left:60px" />
                    </td>
                </tr>
                <tr><td colspan="2"><asp:Button runat="server" id="btnVolDown" Text="Vol Down" OnClick="VolDown_Click" style="width:120px;height:100px;margin-left:60px"/></td></tr>
                <tr>
                    <td><asp:Button runat="server" id="btnLeftKey" Text="Left" OnClick="LeftKey_Click" style="width:120px;height:100px"/></td>
                    <td><asp:Button runat="server" id="btnRightKey" Text="Right" OnClick="RightKey_Click" style="width:120px;height:100px"/></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button runat="server" id="btnSpace" Text="Space" OnClick="Space_Click" style="width:100%;height:100px;align-self:center"/>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button runat="server" id="ButtonPrevious" Text="|<<" OnClick="Previous_Click" style="width:80px;height:100px"/></td>
                    <td><asp:Button runat="server" id="ButtonPausePlay" Text="> ||" OnClick="PlayPause_Click" style="width:80px;height:100px"/></td>
                    <td><asp:Button runat="server" id="ButtonNext" Text=">>|" OnClick="Next_Click" style="width:80px;height:100px"/></td>
                </tr>
                <tr >
                    <td>
                        <asp:Button runat="server" id="btnShutDown" Text="Shut Down" OnClick="Shutdown_Click" style="margin-top:100px"/>
                    </td>
                    <td>
                        <asp:Button runat="server" id="Launchserver" Text="Launch Server" OnClick="LaunchServer_Click" style="margin-top:100px"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    </div>
</asp:Content>
