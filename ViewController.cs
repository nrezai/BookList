using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using UIKit;

namespace AP4
{
	public partial class ViewController : UIViewController
	{
		 private void PopulateTableView()  { 	var db = new SQLiteConnection (filePath); 	// retrieve all the data in the DB table 	var bookList = db.Table<Book>();  	List<string> bookTitles = new List<string> ();  	// loop through the data and retrieve the BookTitle column data only 	foreach (var book in bookList) { 		bookTitles.Add (book.BookTitle); 	} 
       // set the data source for the tableView control 	tblBooks.Source = new BookListTableSource (bookTitles.ToArray ());  }
		partial void BtnAdd_TouchUpInside(UIButton sender)
		{
			 // Input Validation: only insert a book if the title is not empty 			 if (!string.IsNullOrEmpty(txtTitle.Text))  			{  			// Insert a new book into the database
				var newBook = new Book { BookTitle = txtTitle.Text, ISBN = txtISBN.Text };
				var db = new SQLiteConnection(filePath);
				db.Insert (newBook);


                //Create Alert
				var okAlertController = UIAlertController.Create("Success", "bookID: { 0} with Title: { 1} has been added!", UIAlertControllerStyle.Alert);

				//Add Action
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));

				// Present Alert
				PresentViewController(okAlertController, true, null);
       
				
 				// TODO: Add code to populate the Table View with the new values
                PopulateTableView();

				// call this method to refresh the Table View data
				tblBooks.ReloadData ();  			}  			else  			{
			//Create Alert
			var okAlertController = UIAlertController.Create("Failed", "bookID: { 0} with Title: { 1} has not been added!", UIAlertControllerStyle.Alert);

			//Add Action
			okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));

				// Present Alert
				PresentViewController(okAlertController, true, null);  			}
		}

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

			// define the file path on the device where the DB will be stored
			string filePath = Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.Personal), "BookList.db3");

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			// Create our connection, if the database file and/or table doesn't exist create it
			var db = new SQLiteConnection(filePath);
			db.CreateTable<Book>();

 			// TODO: Add code to populate the Table View if it contains data
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
