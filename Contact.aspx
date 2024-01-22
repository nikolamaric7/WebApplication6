<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApplication6.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>O aplikaciji</h2>
    <p>Aplikacija sluzi za rezervaciju autobuskih mesta tako što kliknete na sedište koje je obojeno zelenom bojom jer to znači da je slobodno, crvena ne možete zato što su zauzeta, izaberete zeleno sedište napišete ime prezime i email. Kliknete dugme pošalji i ono automatski postaje rezervisano i niko više ne može da ga zauzme jer postaje obojeno u crveno. U bazu se takodje upisuju podaci o rezervaciji sedišta.</p>
    </asp:Content>
