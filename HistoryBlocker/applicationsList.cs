using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace HistoryBlocker
{
    public class applicationsList : CollectionBase, IBindingList
    {
        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;

        #region iBindingList and CollectionBase
        public fileBlocker this[int index]
        {
            get
            {
                return (fileBlocker)(List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public applicationsList()
        {
            string pathJumpList = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations");
            XDocument xdoc = XDocument.Load("AppIDList.xml");
            DirectoryInfo diTop = new DirectoryInfo(pathJumpList);

            foreach (FileInfo fileJump in diTop.EnumerateFiles("*.automaticDestinations-ms"))
            {
                var query = from application in xdoc.Element("applications").Elements("application")
                            where application.Attribute("AppID").Value == Path.GetFileNameWithoutExtension(fileJump.Name)
                            select application;
                XElement oneApplication = query.SingleOrDefault();

                if(oneApplication != null)
                {
                    List.Add(new fileBlocker(oneApplication.Element("description").Value.ToString(), oneApplication.Attribute("AppID").Value.ToString()));
                }
                else
                {
                    List.Add(new fileBlocker(Path.GetFileNameWithoutExtension(fileJump.Name), Path.GetFileNameWithoutExtension(fileJump.Name)));
                }
            }
        }
        

        public int Add (fileBlocker value) 
		{
            int newIndex = List.Add(value);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded,newIndex));
            return newIndex;
		}

        /*public fileBlocker AddNew()
        {
            return (fileBlocker)((IBindingList)this).AddNew();
        }*/

        public void Remove(fileBlocker value)
        {
            List.Remove(value);
        }

        protected virtual void OnListChanged(ListChangedEventArgs ev)
        {
            if (onListChanged != null)
            {
                onListChanged(this, ev);
            }
        }

        protected override void OnClear()
        {
            foreach (fileBlocker c in List)
            {
                if (c.IsLocked())
                {
                    c.Unlock();
                }
                c.Parent = null;
            }
        }

        protected override void OnClearComplete()
        {
            OnListChanged(resetEvent);
        }

        protected override void OnInsertComplete(int index, object value)
        {
            fileBlocker c = (fileBlocker)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            fileBlocker c = (fileBlocker)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {

                fileBlocker oldcust = (fileBlocker)oldValue;
                fileBlocker newcust = (fileBlocker)newValue;

                oldcust.Parent = null;
                newcust.Parent = this;


                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            }
        }

        // Called by fileBlocker when it changes.
        internal void fileBlockerChanged(fileBlocker blocker)
        {

            int index = List.IndexOf(blocker);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        // Implements IBindingList.
        bool IBindingList.AllowEdit
        {
            get { return false; }
        }

        bool IBindingList.AllowNew
        {
            get { return false; }
        }

        bool IBindingList.AllowRemove
        {
            get { return false; }
        }

        bool IBindingList.SupportsChangeNotification
        {
            get { return true; }
        }

        bool IBindingList.SupportsSearching
        {
            get { return true; }
        }

        bool IBindingList.SupportsSorting
        {
            get { return false; }
        }

        // Events.
        public event ListChangedEventHandler ListChanged
        {
            add
            {
                onListChanged += value;
            }
            remove
            {
                onListChanged -= value;
            }
        }

        // Methods.
        object IBindingList.AddNew()
        {
            /*fileBlocker c = new fileBlocker(this.);
            List.Add(c);
            return c;*/
            throw new NotSupportedException();
        }

        // Unsupported properties.
        bool IBindingList.IsSorted
        {
            get { throw new NotSupportedException(); }
        }

        ListSortDirection IBindingList.SortDirection
        {
            get { throw new NotSupportedException(); }
        }


        PropertyDescriptor IBindingList.SortProperty
        {
            get { throw new NotSupportedException(); }
        }

        // Unsupported Methods.
        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (((fileBlocker)List[i]).AppName == ((string)key))
                    return i;
            }
            return -1;
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort()
        {
            throw new NotSupportedException();
        }

        #endregion

    }
}
