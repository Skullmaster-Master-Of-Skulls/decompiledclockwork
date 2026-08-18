using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000119 RID: 281
	internal class ClientData
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00035374 File Offset: 0x00033574
		private ClientData()
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00035430 File Offset: 0x00033630
		private ClientData(XmlReader reader)
		{
			reader.ReadStartElement("ClientData");
			for (int i = 0; i < 13; i++)
			{
				reader.ReadStartElement(ClientData._StoredValueNames[i]);
				if (this._StoredValues[i] is string)
				{
					this._StoredValues[i] = reader.ReadContentAsString();
				}
				else if (this._StoredValues[i] is DateTime)
				{
					string s = reader.ReadContentAsString();
					long fileTime = long.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					this._StoredValues[i] = DateTime.FromFileTimeUtc(fileTime);
				}
				else if (this._StoredValues[i] is bool)
				{
					string text = reader.ReadContentAsString();
					this._StoredValues[i] = (!string.IsNullOrEmpty(text) && !(text != "1"));
				}
				else
				{
					this._StoredValues[i] = ClientData.ReadStringArray(reader);
				}
				reader.ReadEndElement();
			}
			reader.ReadEndElement();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000355D0 File Offset: 0x000337D0
		private static string[] ReadStringArray(XmlReader reader)
		{
			StringCollection stringCollection = new StringCollection();
			while (reader.IsStartElement())
			{
				reader.ReadStartElement("item");
				stringCollection.Add(reader.ReadContentAsString());
				reader.ReadEndElement();
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00035620 File Offset: 0x00033820
		private static void WriteStringArray(XmlWriter writer, string[] arrToWrite)
		{
			if (arrToWrite.Length == 0)
			{
				writer.WriteValue(string.Empty);
			}
			for (int i = 0; i < arrToWrite.Length; i++)
			{
				writer.WriteStartElement("item");
				writer.WriteValue((arrToWrite[i] == null) ? string.Empty : arrToWrite[i]);
				writer.WriteEndElement();
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00035670 File Offset: 0x00033870
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0003567F File Offset: 0x0003387F
		internal string LastLoggedInUserName
		{
			get
			{
				return (string)this._StoredValues[0];
			}
			set
			{
				this._StoredValues[0] = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0003568A File Offset: 0x0003388A
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00035699 File Offset: 0x00033899
		internal DateTime LastLoggedInDateUtc
		{
			get
			{
				return (DateTime)this._StoredValues[1];
			}
			set
			{
				this._StoredValues[1] = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000356A9 File Offset: 0x000338A9
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x000356B8 File Offset: 0x000338B8
		internal string PasswordHash
		{
			get
			{
				return (string)this._StoredValues[2];
			}
			set
			{
				this._StoredValues[2] = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000356C3 File Offset: 0x000338C3
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x000356D2 File Offset: 0x000338D2
		internal string PasswordSalt
		{
			get
			{
				return (string)this._StoredValues[3];
			}
			set
			{
				this._StoredValues[3] = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000356DD File Offset: 0x000338DD
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000356EC File Offset: 0x000338EC
		internal string[] Roles
		{
			get
			{
				return (string[])this._StoredValues[4];
			}
			set
			{
				this._StoredValues[4] = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000356F7 File Offset: 0x000338F7
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x00035706 File Offset: 0x00033906
		internal DateTime RolesCachedDateUtc
		{
			get
			{
				return (DateTime)this._StoredValues[5];
			}
			set
			{
				this._StoredValues[5] = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00035716 File Offset: 0x00033916
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00035725 File Offset: 0x00033925
		internal string[] SettingsNames
		{
			get
			{
				return (string[])this._StoredValues[6];
			}
			set
			{
				this._StoredValues[6] = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00035730 File Offset: 0x00033930
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0003573F File Offset: 0x0003393F
		internal string[] SettingsStoredAs
		{
			get
			{
				return (string[])this._StoredValues[7];
			}
			set
			{
				this._StoredValues[7] = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003574A File Offset: 0x0003394A
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00035759 File Offset: 0x00033959
		internal string[] SettingsValues
		{
			get
			{
				return (string[])this._StoredValues[8];
			}
			set
			{
				this._StoredValues[8] = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00035764 File Offset: 0x00033964
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00035774 File Offset: 0x00033974
		internal bool SettingsNeedReset
		{
			get
			{
				return (bool)this._StoredValues[9];
			}
			set
			{
				this._StoredValues[9] = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00035785 File Offset: 0x00033985
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00035795 File Offset: 0x00033995
		internal bool SettingsCacheIsMoreFresh
		{
			get
			{
				return (bool)this._StoredValues[10];
			}
			set
			{
				this._StoredValues[10] = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000357A6 File Offset: 0x000339A6
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x000357B6 File Offset: 0x000339B6
		internal string[] CookieNames
		{
			get
			{
				return (string[])this._StoredValues[11];
			}
			set
			{
				this._StoredValues[11] = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000357C2 File Offset: 0x000339C2
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000357D2 File Offset: 0x000339D2
		internal string[] CookieValues
		{
			get
			{
				return (string[])this._StoredValues[12];
			}
			set
			{
				this._StoredValues[12] = value;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000357E0 File Offset: 0x000339E0
		internal void Save()
		{
			if (!this.UsingIsolatedStorage)
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(this.FileName))
				{
					this.Save(xmlWriter);
					return;
				}
			}
			using (IsolatedStorageFile userStoreForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
			{
				if (userStoreForAssembly.GetDirectoryNames("System.Web.Extensions.ClientServices.ClientData").Length == 0)
				{
					userStoreForAssembly.CreateDirectory("System.Web.Extensions.ClientServices.ClientData");
				}
				using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.FileName, FileMode.Create, userStoreForAssembly))
				{
					using (XmlWriter xmlWriter2 = XmlWriter.Create(isolatedStorageFileStream))
					{
						this.Save(xmlWriter2);
					}
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000358A4 File Offset: 0x00033AA4
		private void Save(XmlWriter writer)
		{
			writer.WriteStartElement("ClientData");
			for (int i = 0; i < 13; i++)
			{
				writer.WriteStartElement(ClientData._StoredValueNames[i]);
				if (this._StoredValues[i] == null)
				{
					writer.WriteValue(string.Empty);
				}
				else if (this._StoredValues[i] is string)
				{
					writer.WriteValue(this._StoredValues[i]);
				}
				else if (this._StoredValues[i] is bool)
				{
					writer.WriteValue(((bool)this._StoredValues[i]) ? "1" : "0");
				}
				else if (this._StoredValues[i] is DateTime)
				{
					writer.WriteValue(((DateTime)this._StoredValues[i]).ToFileTimeUtc().ToString("X", CultureInfo.InvariantCulture));
				}
				else
				{
					ClientData.WriteStringArray(writer, (string[])this._StoredValues[i]);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.Flush();
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000359B0 File Offset: 0x00033BB0
		internal static ClientData Load(string username, bool useIsolatedStorage)
		{
			ClientData clientData = null;
			string text = null;
			if (useIsolatedStorage)
			{
				text = "System.Web.Extensions.ClientServices.ClientData\\" + SqlHelper.GetPartialDBFileName(username, ".clientdata");
				try
				{
					using (IsolatedStorageFile userStoreForAssembly = IsolatedStorageFile.GetUserStoreForAssembly())
					{
						using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(text, FileMode.Open, userStoreForAssembly))
						{
							using (XmlReader xmlReader = XmlReader.Create(isolatedStorageFileStream))
							{
								clientData = new ClientData(xmlReader);
							}
						}
					}
					goto IL_B7;
				}
				catch
				{
					goto IL_B7;
				}
			}
			text = SqlHelper.GetFullDBFileName(username, ".clientdata");
			try
			{
				if (File.Exists(text))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
					{
						using (XmlReader xmlReader2 = XmlReader.Create(fileStream))
						{
							clientData = new ClientData(xmlReader2);
						}
					}
				}
			}
			catch
			{
			}
			IL_B7:
			if (clientData == null)
			{
				clientData = new ClientData();
			}
			clientData.UsingIsolatedStorage = useIsolatedStorage;
			clientData.FileName = text;
			return clientData;
		}

		// Token: 0x04000427 RID: 1063
		private const int _NumStoredValues = 13;

		// Token: 0x04000428 RID: 1064
		private static string[] _StoredValueNames = new string[]
		{
			"LastLoggedInUserName",
			"LastLoggedInDateUtc",
			"PasswordHash",
			"PasswordSalt",
			"Roles",
			"RolesCachedDateUtc",
			"SettingsNames",
			"SettingsStoredAs",
			"SettingsValues",
			"SettingsNeedReset",
			"SettingsCacheIsMoreFresh",
			"CookieNames",
			"CookieValues"
		};

		// Token: 0x04000429 RID: 1065
		private object[] _StoredValues = new object[]
		{
			"",
			DateTime.UtcNow.AddYears(-1),
			string.Empty,
			string.Empty,
			new string[0],
			DateTime.UtcNow.AddYears(-1),
			new string[0],
			new string[0],
			new string[0],
			false,
			false,
			new string[0],
			new string[0]
		};

		// Token: 0x0400042A RID: 1066
		private string FileName = string.Empty;

		// Token: 0x0400042B RID: 1067
		private bool UsingIsolatedStorage;

		// Token: 0x0400042C RID: 1068
		private const string _IsolatedDir = "System.Web.Extensions.ClientServices.ClientData";

		// Token: 0x02000180 RID: 384
		internal enum ClientDateStoreOrderEnum
		{
			// Token: 0x04000522 RID: 1314
			LastLoggedInUserName,
			// Token: 0x04000523 RID: 1315
			LastLoggedInDateUtc,
			// Token: 0x04000524 RID: 1316
			PasswordHash,
			// Token: 0x04000525 RID: 1317
			PasswordSalt,
			// Token: 0x04000526 RID: 1318
			Roles,
			// Token: 0x04000527 RID: 1319
			RolesCachedDateUtc,
			// Token: 0x04000528 RID: 1320
			SettingsNames,
			// Token: 0x04000529 RID: 1321
			SettingsStoredAs,
			// Token: 0x0400052A RID: 1322
			SettingsValues,
			// Token: 0x0400052B RID: 1323
			SettingsNeedReset,
			// Token: 0x0400052C RID: 1324
			SettingsCacheIsMoreFresh,
			// Token: 0x0400052D RID: 1325
			CookieNames,
			// Token: 0x0400052E RID: 1326
			CookieValues
		}
	}
}
