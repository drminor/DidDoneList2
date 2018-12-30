using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

/// <remarks>
/// This is copied, with minor changes, from https://github.com/danielmoore/WpfIgniter
/// The original used the CallerMemberName attribute on the PropertyName parameter so that
/// the caller would not have to provide a value if calling from a PropertySetter with the same name.
/// This implementation expects the caller to use the nameof method to specify the name of the property.
/// </remarks>


// TODO: Make this inherit from BindableObject. 


namespace DidDoneListApp.VMBase
{
    /// <summary>
    /// Provides a basic implementation for <see cref="INotifyPropertyChanged"/> and <see cref="INotifyPropertyChanging"/>.
    /// </summary>
    public abstract class BindableBaseWI : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging = delegate { };

        /// <summary>
        /// Assigns the specified value to the specified backing store if a change has
        /// been made and, optionally, raises callbacks before and after.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingStore">The backing store.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="onChanged">An optional callback to raise just before <see cref="PropertyChanged"/>.</param>
        /// <param name="onChanging">An optional callback to raise just before <see cref="PropertyChanging"/>.</param>
        /// <param name="coerceValue">An optional callback to coerce values before they are set.</param>
        protected void SetProperty<T>(string propertyName, ref T backingStore, T value, Action onChanged = null, Action<T> onChanging = null, Func<T, T> coerceValue = null)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");

            var effectiveValue = coerceValue != null ? coerceValue(value) : value;

            if (EqualityComparer<T>.Default.Equals(backingStore, effectiveValue))
            {
                // TODO: Do we really need to raise this PropertyChanged event?
                // This is the only item that requires a reference to WindowsBase.

                // If we coerced this value and the coerced value is not equal to the original, we need to
                // send a fake PropertyChanged event to notify WPF that this value isn't what it thinks it is.
                if (coerceValue != null && !EqualityComparer<T>.Default.Equals(value, effectiveValue))
                {
                    System.Diagnostics.Debug.WriteLine("The effective value is not the same as the assigned value and we are not raising a PropertyChagned event.");
                    //PropertyChangedEventManagerProxy.Instance.RaisePropertyChanged(this, propertyName);
                }

                return;
            }

            onChanging?.Invoke(effectiveValue);

            OnPropertyChanging(propertyName);

            var oldValue = backingStore;
            backingStore = effectiveValue;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName, oldValue, effectiveValue);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        protected void OnPropertyChanged<T>(string propertyName, T oldValue, T newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged(this, args);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void OnPropertyChanging(string propertyName)
        {
            OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.ComponentModel.PropertyChangingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging(this, args);
        }

        //private class PropertyChangedEventManagerProxy
        //{
        //    private readonly NotifyPropertyChangedProxy _notifyPropertyChangedProxy;

        //    // We need to hold on to this ref to keep it from getting GC'd
        //    private readonly IWeakEventListener _weakEventListener;

        //    private PropertyChangedEventManagerProxy()
        //    {
        //        _notifyPropertyChangedProxy = new NotifyPropertyChangedProxy();
        //        _weakEventListener = new WeakListenerStub();

        //        PropertyChangedEventManager.AddListener(_notifyPropertyChangedProxy, _weakEventListener, string.Empty);
        //    }

        //    public void RaisePropertyChanged(object sender, string propertyName)
        //    {
        //        _notifyPropertyChangedProxy.Raise(sender, new PropertyChangedEventArgs(propertyName));
        //    }

        //    private static PropertyChangedEventManagerProxy _instance;
        //    public static PropertyChangedEventManagerProxy Instance { get { return _instance ?? (_instance = new PropertyChangedEventManagerProxy()); } }

        //    private class NotifyPropertyChangedProxy : INotifyPropertyChanged
        //    {
        //        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //        public void Raise(object sender, PropertyChangedEventArgs e)
        //        {
        //            PropertyChanged(sender, e);
        //        }
        //    }

        //    private class WeakListenerStub : IWeakEventListener
        //    {
        //        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e) { return false; }
        //    }
        //}
    }
}


