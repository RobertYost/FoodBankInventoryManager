﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodBankInventoryManager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="FoodBankDB")]
	public partial class L2S_FoodBankDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBin(Bin instance);
    partial void UpdateBin(Bin instance);
    partial void DeleteBin(Bin instance);
    partial void InsertFood(Food instance);
    partial void UpdateFood(Food instance);
    partial void DeleteFood(Food instance);
    partial void InsertInventoryEntry(InventoryEntry instance);
    partial void UpdateInventoryEntry(InventoryEntry instance);
    partial void DeleteInventoryEntry(InventoryEntry instance);
    partial void InsertShelf(Shelf instance);
    partial void UpdateShelf(Shelf instance);
    partial void DeleteShelf(Shelf instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public L2S_FoodBankDBDataContext() : 
				base(global::FoodBankInventoryManager.Properties.Settings.Default.FoodBankDBConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public L2S_FoodBankDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2S_FoodBankDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2S_FoodBankDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2S_FoodBankDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Bin> Bins
		{
			get
			{
				return this.GetTable<Bin>();
			}
		}
		
		public System.Data.Linq.Table<Food> Foods
		{
			get
			{
				return this.GetTable<Food>();
			}
		}
		
		public System.Data.Linq.Table<InventoryEntry> InventoryEntries
		{
			get
			{
				return this.GetTable<InventoryEntry>();
			}
		}
		
		public System.Data.Linq.Table<Shelf> Shelfs
		{
			get
			{
				return this.GetTable<Shelf>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Bin")]
	public partial class Bin : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _BinId;
		
		private EntitySet<InventoryEntry> _InventoryEntries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBinIdChanging(string value);
    partial void OnBinIdChanged();
    #endregion
		
		public Bin()
		{
			this._InventoryEntries = new EntitySet<InventoryEntry>(new Action<InventoryEntry>(this.attach_InventoryEntries), new Action<InventoryEntry>(this.detach_InventoryEntries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BinId", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string BinId
		{
			get
			{
				return this._BinId;
			}
			set
			{
				if ((this._BinId != value))
				{
					this.OnBinIdChanging(value);
					this.SendPropertyChanging();
					this._BinId = value;
					this.SendPropertyChanged("BinId");
					this.OnBinIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Bin_InventoryEntry", Storage="_InventoryEntries", ThisKey="BinId", OtherKey="BinId")]
		public EntitySet<InventoryEntry> InventoryEntries
		{
			get
			{
				return this._InventoryEntries;
			}
			set
			{
				this._InventoryEntries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Bin = this;
		}
		
		private void detach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Bin = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Food")]
	public partial class Food : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _FoodId;
		
		private string _FoodName;
		
		private int _AverageQty;
		
		private EntitySet<InventoryEntry> _InventoryEntries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFoodIdChanging(string value);
    partial void OnFoodIdChanged();
    partial void OnFoodNameChanging(string value);
    partial void OnFoodNameChanged();
    partial void OnAverageQtyChanging(int value);
    partial void OnAverageQtyChanged();
    #endregion
		
		public Food()
		{
			this._InventoryEntries = new EntitySet<InventoryEntry>(new Action<InventoryEntry>(this.attach_InventoryEntries), new Action<InventoryEntry>(this.detach_InventoryEntries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FoodId", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string FoodId
		{
			get
			{
				return this._FoodId;
			}
			set
			{
				if ((this._FoodId != value))
				{
					this.OnFoodIdChanging(value);
					this.SendPropertyChanging();
					this._FoodId = value;
					this.SendPropertyChanged("FoodId");
					this.OnFoodIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FoodName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FoodName
		{
			get
			{
				return this._FoodName;
			}
			set
			{
				if ((this._FoodName != value))
				{
					this.OnFoodNameChanging(value);
					this.SendPropertyChanging();
					this._FoodName = value;
					this.SendPropertyChanged("FoodName");
					this.OnFoodNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AverageQty", DbType="Int NOT NULL")]
		public int AverageQty
		{
			get
			{
				return this._AverageQty;
			}
			set
			{
				if ((this._AverageQty != value))
				{
					this.OnAverageQtyChanging(value);
					this.SendPropertyChanging();
					this._AverageQty = value;
					this.SendPropertyChanged("AverageQty");
					this.OnAverageQtyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Food_InventoryEntry", Storage="_InventoryEntries", ThisKey="FoodId", OtherKey="FoodId")]
		public EntitySet<InventoryEntry> InventoryEntries
		{
			get
			{
				return this._InventoryEntries;
			}
			set
			{
				this._InventoryEntries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Food = this;
		}
		
		private void detach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Food = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.InventoryEntry")]
	public partial class InventoryEntry : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _EntryId;
		
		private int _UserId;
		
		private string _FoodId;
		
		private string _BinId;
		
		private string _ShelfId;
		
		private int _BinQty;
		
		private System.DateTime _DateEntered;
		
		private EntityRef<Bin> _Bin;
		
		private EntityRef<Food> _Food;
		
		private EntityRef<Shelf> _Shelf;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEntryIdChanging(int value);
    partial void OnEntryIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnFoodIdChanging(string value);
    partial void OnFoodIdChanged();
    partial void OnBinIdChanging(string value);
    partial void OnBinIdChanged();
    partial void OnShelfIdChanging(string value);
    partial void OnShelfIdChanged();
    partial void OnBinQtyChanging(int value);
    partial void OnBinQtyChanged();
    partial void OnDateEnteredChanging(System.DateTime value);
    partial void OnDateEnteredChanged();
    #endregion
		
		public InventoryEntry()
		{
			this._Bin = default(EntityRef<Bin>);
			this._Food = default(EntityRef<Food>);
			this._Shelf = default(EntityRef<Shelf>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EntryId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int EntryId
		{
			get
			{
				return this._EntryId;
			}
			set
			{
				if ((this._EntryId != value))
				{
					this.OnEntryIdChanging(value);
					this.SendPropertyChanging();
					this._EntryId = value;
					this.SendPropertyChanged("EntryId");
					this.OnEntryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FoodId", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FoodId
		{
			get
			{
				return this._FoodId;
			}
			set
			{
				if ((this._FoodId != value))
				{
					if (this._Food.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFoodIdChanging(value);
					this.SendPropertyChanging();
					this._FoodId = value;
					this.SendPropertyChanged("FoodId");
					this.OnFoodIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BinId", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string BinId
		{
			get
			{
				return this._BinId;
			}
			set
			{
				if ((this._BinId != value))
				{
					if (this._Bin.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBinIdChanging(value);
					this.SendPropertyChanging();
					this._BinId = value;
					this.SendPropertyChanged("BinId");
					this.OnBinIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShelfId", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ShelfId
		{
			get
			{
				return this._ShelfId;
			}
			set
			{
				if ((this._ShelfId != value))
				{
					if (this._Shelf.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnShelfIdChanging(value);
					this.SendPropertyChanging();
					this._ShelfId = value;
					this.SendPropertyChanged("ShelfId");
					this.OnShelfIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BinQty", DbType="Int NOT NULL")]
		public int BinQty
		{
			get
			{
				return this._BinQty;
			}
			set
			{
				if ((this._BinQty != value))
				{
					this.OnBinQtyChanging(value);
					this.SendPropertyChanging();
					this._BinQty = value;
					this.SendPropertyChanged("BinQty");
					this.OnBinQtyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateEntered", DbType="DateTime NOT NULL")]
		public System.DateTime DateEntered
		{
			get
			{
				return this._DateEntered;
			}
			set
			{
				if ((this._DateEntered != value))
				{
					this.OnDateEnteredChanging(value);
					this.SendPropertyChanging();
					this._DateEntered = value;
					this.SendPropertyChanged("DateEntered");
					this.OnDateEnteredChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Bin_InventoryEntry", Storage="_Bin", ThisKey="BinId", OtherKey="BinId", IsForeignKey=true)]
		public Bin Bin
		{
			get
			{
				return this._Bin.Entity;
			}
			set
			{
				Bin previousValue = this._Bin.Entity;
				if (((previousValue != value) 
							|| (this._Bin.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Bin.Entity = null;
						previousValue.InventoryEntries.Remove(this);
					}
					this._Bin.Entity = value;
					if ((value != null))
					{
						value.InventoryEntries.Add(this);
						this._BinId = value.BinId;
					}
					else
					{
						this._BinId = default(string);
					}
					this.SendPropertyChanged("Bin");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Food_InventoryEntry", Storage="_Food", ThisKey="FoodId", OtherKey="FoodId", IsForeignKey=true)]
		public Food Food
		{
			get
			{
				return this._Food.Entity;
			}
			set
			{
				Food previousValue = this._Food.Entity;
				if (((previousValue != value) 
							|| (this._Food.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Food.Entity = null;
						previousValue.InventoryEntries.Remove(this);
					}
					this._Food.Entity = value;
					if ((value != null))
					{
						value.InventoryEntries.Add(this);
						this._FoodId = value.FoodId;
					}
					else
					{
						this._FoodId = default(string);
					}
					this.SendPropertyChanged("Food");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Shelf_InventoryEntry", Storage="_Shelf", ThisKey="ShelfId", OtherKey="ShelfId", IsForeignKey=true)]
		public Shelf Shelf
		{
			get
			{
				return this._Shelf.Entity;
			}
			set
			{
				Shelf previousValue = this._Shelf.Entity;
				if (((previousValue != value) 
							|| (this._Shelf.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Shelf.Entity = null;
						previousValue.InventoryEntries.Remove(this);
					}
					this._Shelf.Entity = value;
					if ((value != null))
					{
						value.InventoryEntries.Add(this);
						this._ShelfId = value.ShelfId;
					}
					else
					{
						this._ShelfId = default(string);
					}
					this.SendPropertyChanged("Shelf");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_InventoryEntry", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.InventoryEntries.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.InventoryEntries.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Shelf")]
	public partial class Shelf : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ShelfId;
		
		private EntitySet<InventoryEntry> _InventoryEntries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnShelfIdChanging(string value);
    partial void OnShelfIdChanged();
    #endregion
		
		public Shelf()
		{
			this._InventoryEntries = new EntitySet<InventoryEntry>(new Action<InventoryEntry>(this.attach_InventoryEntries), new Action<InventoryEntry>(this.detach_InventoryEntries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShelfId", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string ShelfId
		{
			get
			{
				return this._ShelfId;
			}
			set
			{
				if ((this._ShelfId != value))
				{
					this.OnShelfIdChanging(value);
					this.SendPropertyChanging();
					this._ShelfId = value;
					this.SendPropertyChanged("ShelfId");
					this.OnShelfIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Shelf_InventoryEntry", Storage="_InventoryEntries", ThisKey="ShelfId", OtherKey="ShelfId")]
		public EntitySet<InventoryEntry> InventoryEntries
		{
			get
			{
				return this._InventoryEntries;
			}
			set
			{
				this._InventoryEntries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Shelf = this;
		}
		
		private void detach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.Shelf = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private string _FirstName;
		
		private string _LastName;
		
		private string _Password;
		
		private EntitySet<InventoryEntry> _InventoryEntries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public User()
		{
			this._InventoryEntries = new EntitySet<InventoryEntry>(new Action<InventoryEntry>(this.attach_InventoryEntries), new Action<InventoryEntry>(this.detach_InventoryEntries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_InventoryEntry", Storage="_InventoryEntries", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<InventoryEntry> InventoryEntries
		{
			get
			{
				return this._InventoryEntries;
			}
			set
			{
				this._InventoryEntries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_InventoryEntries(InventoryEntry entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
#pragma warning restore 1591
