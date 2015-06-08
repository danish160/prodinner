<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (HttpContext.Current.User.Identity.IsAuthenticated)
   { %>
<span class="optional">Welcome</span>
<%=HttpContext.Current.User.Identity.Name %>
<%=Html.ActionLink("Log out", "SignOff","account") %>
<%}
   else
   {%>
<%=Html.ActionLink("log in", "SignIn","account") %>
<%}%>
