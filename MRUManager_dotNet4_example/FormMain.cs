using System;
using System.Windows.Forms;
using System.IO;

namespace MRUManager_dotNet4_example
{
  public partial class FormMain : Form
  {
    private MRUManager mruManager;

    public FormMain()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      mruManager = new MRUManager(
        //the menu item that will contain the recent files
        recentFilesToolStripMenuItem,

        //the name of your program
        "myProgram",

        //the funtion that will be called when a recent file gets clicked.
        myOwnRecentFileGotClicked_handler,

        //an optional function to call when the user clears the list of recent items
        myOwnRecentFilesGotCleared_handler);
    }

    private void myOwnRecentFileGotClicked_handler(object obj, EventArgs evt)
    {
      string fName = (obj as ToolStripItem).Text;
      if (!File.Exists(fName))
      {
        if (MessageBox.Show(string.Format("{0} doesn't exist. Remove from recent workspaces?", fName), "File not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          mruManager.RemoveRecentFile(fName);
        }

        return;
      }

      //do something with the file here
      MessageBox.Show(string.Format("Through the 'Recent Files' menu item, you opened: {0}", fName));
    }

    private void myOwnRecentFilesGotCleared_handler(object obj, EventArgs evt)
    {
      //prior to this function getting called, all recent files in the registry and 
      //in the program's 'Recent Files' menu are cleared.

      //perhaps you want to do something here after all this happens
      MessageBox.Show("You just cleared all recent files.");
    }

    private void openToolStripMenuItem_Click(object obj, EventArgs evt)
    {
      FileDialog openFileDlg = new OpenFileDialog();
      openFileDlg.InitialDirectory = Environment.CurrentDirectory;
      if (openFileDlg.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      string openedFile = openFileDlg.FileName;

      //Now give it to the MRUManager
      mruManager.AddRecentFile(openedFile);

      //do something with the file here
      MessageBox.Show("Through the 'Open' menu item, you opened: " + openedFile);
    }
  }
}