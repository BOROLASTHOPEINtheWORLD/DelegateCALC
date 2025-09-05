using DelegateCALC.Commands;
using DelegateCALC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// Основная VM, связывает модель с представлениями
namespace DelegateCALC.ViewModels
{
    public class CalculatorViewModel : ObservableObject
    {
		private CalculatorModel _model;

		public CalculatorViewModel()
		{
			_model = new CalculatorModel();
		}

		public string DisplayText 
		{
			get => _model.CurrentInput;
			set
			{
				OnPropertyChanged();
			}
		}
		public ICommand NumberCommand => new RelayCommand<string>(number =>
		{
			_model.AppendNumber(number);
			OnPropertyChanged(nameof(DisplayText));
		});

		public ICommand OperationCommand => new RelayCommand<string>(operation =>
		{
			_model.SetOperation(operation);
			OnPropertyChanged(nameof(DisplayText));
		});

		public ICommand EqualsCommand => new RelayCommand(() =>
		{
			_model.Calculate();
			OnPropertyChanged(nameof(DisplayText));
		});
		public ICommand BackspaceCommand => new RelayCommand(() =>
		{
			_model.Erase();
			OnPropertyChanged(nameof(DisplayText));
		});
		public ICommand NegateCommand => new RelayCommand(() =>
		{
			_model.Negate();
			OnPropertyChanged(nameof(DisplayText));
		});
		

		public ICommand ClearCommand => new RelayCommand(() =>
		{
			_model.Clear();
			OnPropertyChanged(nameof(DisplayText));	
		});
		
	}
}
