using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WebApiClientLib;
using Xamarin.Forms;

namespace DidDoneListApp.ViewModels
{
    public class RandomIntegerViewModel : INotifyPropertyChanged
    {
        private ICommand _generateRandomInteger;
        private IIntegerGenerator _integerGeneratorService;
        private int _luckyNumber;
        public RandomIntegerViewModel(IIntegerGenerator integerGeneratorService)
        {
            _integerGeneratorService = integerGeneratorService;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GenerateRandomInteger
        {
            get
            {
                if (_generateRandomInteger == null)
                {
                    _generateRandomInteger = new Command(() =>
                    {
                        LuckyNumber = _integerGeneratorService.GenerateInt();
                    });
                }
                return _generateRandomInteger;
            }
        }
        public int LuckyNumber
        {
            get
            {
                return _luckyNumber;
            }
            private set
            {
                if (_luckyNumber != value)
                {
                    _luckyNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LuckyNumber)));
                }
            }
        }
    }
}
