using DidDoneListApp.VMBase;

namespace DidDoneListApp.Models
{
    public class Customer : ViewModelBase
    {

        private int _id;
        public int Id
        {
            get => _id;
            set { SetProperty(nameof(Id), ref _id, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { SetProperty(nameof(FirstName), ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { SetProperty(nameof(LastName), ref _lastName, value); }
        }

        private string _streetAddress;
        public string StreetAddress
        {
            get => _streetAddress;
            set { SetProperty(nameof(StreetAddress), ref _streetAddress, value); }
        }

        private string _city;
        public string City
        {
            get => _city;
            set { SetProperty(nameof(City), ref _city, value); }
        }

        private int _stateId;
        public int StateId
        {
            get => _stateId;
            set { SetProperty(nameof(StateId), ref _stateId, value); }
        }

        private string _zip;
        public string Zip
        {
            get => _zip;
            set { SetProperty(nameof(Zip), ref _zip, value); }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set { SetProperty(nameof(EmailAddress), ref _emailAddress, value); }
        }

        private bool _active;
        public bool Active
        {
            get => _active;
            set { SetProperty(nameof(Active), ref _active, value); }
        }


    }
}
