using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using diaryApp.Views;
using diaryApp.DB;

namespace diaryApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            // MainPage = new AppShell();
            // 指紋認証ページ
            MainPage = new NavigationPage(new TouchIDPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        // 日記データベースを開くソース
        static string diaryDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "diary.db3");
        static DiaryDAO diaryDatabase;
        public static DiaryDAO diaryDAO
        {
            get
            {
                if(diaryDatabase == null)
                {
                    diaryDatabase = new DiaryDAO(diaryDbPath);
                }
                return diaryDatabase;
            }
        }
    }
}
