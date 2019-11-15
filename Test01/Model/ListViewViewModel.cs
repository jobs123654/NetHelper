using ControlEase.Nexus.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Test01.Model;

namespace Test01
{
     public class MainViewModel:ObservableObject
    {
        #region 变量
        private ObservableCollection<ItemModel> mItems=new ObservableCollection<ItemModel>();
        private ListView mListView;
        public MainViewModel()
        {
        }
        public MainViewModel(ListView listView)
        {
            mListView = listView;
        }
        #endregion

        #region 属性
        public RelayCommand InitItemsSourceCommand
        {
            get => new RelayCommand(InitItemsSource);
        }
        public RelayCommand ChangeItemsSourceCommand
        {
            get => new RelayCommand(ChangeItemsSource);
        }
        public ObservableCollection<ItemModel> Items
        {
            get => mItems; set
            {
                mItems = value;
                OnPropertyChanged("Items");
            }
        }
        #endregion

        #region 方法
        #region 构造
       
        public void InitItemsSource()
        {
            Items.Clear();
            for (int i = 0; i < 4; i++)
            {
                Items.Add(new ItemModel
                {
                    Number = i,
                    Pic = "../Resources/ico.ico"
                });
            }
        }
        public void ChangeItemsSource()
        {
            Items.Clear();
            if (mListView != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Items.Add(new ItemModel
                    {
                        Number = i,
                        Pic = "../Resources/ico.ico"
                    });
                }
            }
            mListView.ScrollIntoView(Items[0]);
            
        }
        #endregion
        #endregion



    }
}
