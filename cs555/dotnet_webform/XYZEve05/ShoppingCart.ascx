<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCart.ascx.cs" Inherits="ShoppingCart" %>
<table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr>
        <td>
            <hr color="#0000cc" size="3" width="100%" />
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Table ID="tblCart" runat="server">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td>
            <p>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" BackColor="#C0FFC0" Text="Clear All" Width="96px" OnClick="btnClear_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" BackColor="Yellow" Height="28px" Text="Update Cart"
                    Width="130px" OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" BackColor="Red" Text="Cancel Changes"
                    Width="144px" ForeColor="Yellow" OnClick="btnCancel_Click" /></p>
            <p>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <font color="#006600">(To remove an individual item,
                    set the quantity to 0)</font></p>
        </td>
    </tr>
    <tr>
        <td>
            <hr color="#0000cc" size="3" width="100%" />
            &nbsp;</td>
    </tr>
</table>
