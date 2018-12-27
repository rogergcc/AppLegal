using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppLegal.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {

        }

        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(
         ref T backingStore, T value,
         [CallerMemberName]string propertyName = "",
         Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            return true;


        }


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
