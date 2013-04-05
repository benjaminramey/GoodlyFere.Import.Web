# GoodlyFere.Import.Web

Web interface to GoodlyFere Import framework.

## Installation and setup
1. Create a new IIS website and point it to a new folder somewhere on your
hard drive.
2. Change the app pool associated with the new website to use .NET 4.0.
3. Download the latest package under the build/ directory in this repository.
The packages are named "ImportWebPackage_xxxx.zip" where the "xxxx" is the
version number.
4. Unzip the package into the directory for your newly created website.
5. Edit the Web.config to change the two connection strings to point to
your desired database.  By default, they point to a local MS SQL instance
named "SQLEXPRESS" or "SQLEXPRESS_R2".  This project uses EntityFramework.  So,
as long as you setup a connection string to an EntityFramework-compatible
database, it should work.  Read [Elmah documentation](https://code.google.com/p/elmah) to configure Elmah
for other databases.
6. If you are using MS SQL and Integrated Security (default), make sure to
add the account that your website app pool runs under to
the "dbcreator" role in your MS SQL instance.  If using another database or
different security setup, ensure your user has database creation permissions.  Note that
if you are using SQLite, you must create the database file manually.
7. Access the website in your browser, this will trigger EntityFramework to create the "Import"
database.
8. Download the appropriate Elmah script at their [download page](https://code.google.com/p/elmah/wiki/Downloads).
Run the script to setup your Elmah table in the database you pointed it to (default is the same "Import" db
that EntityFramework creates).

## Version History
- (1.0.2.1) Fixed styling for notification center to not cover content up
- (1.0.2.0) Updated UI with notifications center
- (1.0.1.0) Updated GoodlyFere.Import dependency to 1.0.1.0
- (1.0.0.10) Implemented a basic reload for plugins with new package architecture
