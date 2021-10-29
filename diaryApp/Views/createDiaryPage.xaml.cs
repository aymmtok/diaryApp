using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using diaryApp.DB;

namespace diaryApp.Views
{
    public partial class createDiaryPage : ContentPage
    {
        //選択された日記のID格納（新規作成は０）
        private int _checkedDiaryID = 0;

        public createDiaryPage()
        {
            InitializeComponent();
        }

        // 過去の日記データが渡された場合、それらのデータを作成画面に反映
        public createDiaryPage(Diary diary)
        {
            InitializeComponent();
            SetDiaryInfo(diary);
        }

        private void SetDiaryInfo(Diary diary)
        {
            _checkedDiaryID = diary.DiaryID;
            Date.Date = diary.Date;
            Title.Text = diary.Title;
            Detail.Text = diary.Detail;
        }

        // 戻るボタンの処理
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        // 保存ボタンの処理
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // 入力されたテキストの取得
            DateTime dateTime = Date.Date;
            string titleText = Title.Text;
            string detailText = Detail.Text;

            // 入力された内容を表示
            var result = await DisplayAlert(titleText+"("+dateTime.ToString("yyyy/MM/dd")+")",detailText, "OK", "キャンセル");

            // OKが押されたら保存
            if (result)
            {
                // DAOを用いて入力されたデータをDBに保存する
                await App.diaryDAO.SaveDiaryAsync(
                    new DB.Diary
                    {
                        Date = Date.Date,
                        Title = Title.Text,
                        Detail = Detail.Text
                    });

                // セーブが完了したら元画面に自動的に戻る
                await Navigation.PopModalAsync();
            }
        }
    }
}
