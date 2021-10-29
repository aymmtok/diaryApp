using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace diaryApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            testLabel.Text = "Button Clicked!";
        }

        void OnEntryButtonClicked(object sender, EventArgs e)
        {
            entryLabel.Text = testEntry.Text;
        }

        void OnEditorButtonClicked(object sender, EventArgs e)
        {
            editorLabel.Text = testEditor.Text;
        }
    }
}
