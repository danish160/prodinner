http://prodinner.codeplex.com

requires: .net 4.5, asp.net mvc 5, VS2012, sql server
uses the Trial version of ASP.net MVC Awesome ( http://aspnetawesome.com)

------------
create the database by executing the db.sql script 

(open it in sql management studio, hit f5)
(you are going to get some errors like: "can not drop the database prodinner" or "only user processes can be killed" it's ok )
the script tries to kill all connections to prodinner, drop the db, and create it, after it inserts some data

------------

start the solution (ProDinner.sln)

edit the connection string from WebUI\web.config 

(if it's needed, probably you will need to change the Data Source, now it's .\sqlexpress, also username and password, now they are UID=sa;pwd=1)


****************
at this moment everything should work but you might get generic GDI+ error when trying to upload image, to get rid of this error do this:
go to properties of \WebUI\pictures folder and in security tab add full control rights for the IIS_IUSRS
(on Win7 and 2008 server it's properties-> security tab -> Edit button -> Add button -> Advanced button -> Find Now button -> select IIS_IUSRS from the search results -> OK button -> OK button -> Full Control checkbox -> OK -> OK )


==================
in _Layout.cshtml a js function is assigned to awe.err, this will show popup in the left bottom corner of the screen any time an ajax request to the server of an Awesome helperwill ecounter an error

changing themes is being handled by the ChangeThemeController and a bit of js in _Layout.cshtml

in IE hitting Enter in a textbox won't trigger change, a fix for that is in Site.js

JSON2.js is to support IE7, IE8 with some Doctypes might need it as well
