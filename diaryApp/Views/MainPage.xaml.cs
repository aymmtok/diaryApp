using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diaryApp.DB;
using System.Text;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace diaryApp.Views
{
    public partial class MainPage : ContentPage
    {
        // 編集モードかどうかの状態を保持
        private bool IsEditMode = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // ソートされた日記一覧をdiaryListのItemSourceに設定
            diaryList.ItemsSource = await App.diaryDAO.GetDiaryAsyncSorted();
        }

        private void OnNewDiaryButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new createDiaryPage());
        }

        // 過去の日記データが選択された時、その日記データを編集する画面へ遷移する
        private void TappedDiaryListItem(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as DB.Diary;
            Navigation.PushModalAsync(new createDiaryPage(item));
        }

        // 編集モードを変更する
        private async void ChangeMode()
        {
            // データベースのデータ取得
            List<Diary> temp = await App.diaryDAO.GetDiaryAsyncSorted();

            // IsEditModeを変更する
            IsEditMode = !IsEditMode;
            // IsEditModeに応じて、表示内容を変更
            DeleteButton.IsVisible = IsEditMode;
            EditButton.Text = IsEditMode ? "戻る" : "編集";

            // 各データにeditMode及びisCheckedを反映
            foreach(var a in temp)
            {
                a.editMode = IsEditMode;

                if (!IsEditMode)
                {
                    a.isChecked = false;
                }
            }

            // 表示
            diaryList.ItemsSource = temp;
        }

        // 編集ボタン/戻るボタンが押された時の処理
        private void OnEditButtonClicked(object sender, EventArgs e)
        {
            // 編集モードを変更
            ChangeMode();
        }

        // 削除ボタンが押された時の処理
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // 確認ダイアログの表示
            bool isDelete = await DisplayAlert("削除の確認", "選択した項目を削除してよいですか？","削除する","キャンセル");

            // 「削除する」が選択された場合は削除処理の実行
            if (isDelete)
            {
                // 現在の表示されているdiaryListを取得
                List<Diary> temp = (List<Diary>)diaryList.ItemsSource;

                // チェックが入っている要素を削除
                for(int i=0; i<temp.Count; i++)
                {
                    if (temp[i].isChecked)
                    {
                        await App.diaryDAO.DeleteDiaryAsync(temp[i]);
                    }
                }

                // 削除後のデータベース情報を取得し、反映
                temp = await App.diaryDAO.GetDiaryAsyncSorted();
                diaryList.ItemsSource = temp;

                // 編集モードを変更
                ChangeMode();
            }
        }
    }
}
