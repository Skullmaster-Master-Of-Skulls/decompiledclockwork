using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Windows.Forms;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.ClientCaching
{
	// Token: 0x02000002 RID: 2
	public class ClientCache : ICacheStorageManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Use ObjectFactory.Resolve<ClientCache>() instead")]
		public static ClientCache CurrentInstance { get; } = ObjectFactory.Resolve<ClientCache>();

		// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public ClientCache()
		{
			this._baseCache = ObjectFactory.Resolve<ICacheStorageManager>();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
		public void RemoveAllSubItems(eServerCacheItemType key)
		{
			this._baseCache.RemoveAllSubItems(key);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
		public void ClearCache()
		{
			this._baseCache.ClearCache();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002085 File Offset: 0x00000285
		public object[] Keys { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000208D File Offset: 0x0000028D
		public int CountItems
		{
			get
			{
				return this._baseCache.CountItems;
			}
		}

		// Token: 0x17000004 RID: 4
		public object this[object key]
		{
			get
			{
				return this._baseCache[key];
			}
			set
			{
				this._baseCache[key] = value;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B7 File Offset: 0x000002B7
		public void Insert(object key, object value)
		{
			this._baseCache.Insert(key, value);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020C6 File Offset: 0x000002C6
		public void Insert(object key, object value, DateTime expirationDate)
		{
			this._baseCache.Insert(key, value, expirationDate);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D6 File Offset: 0x000002D6
		public void Insert(object key, object value, TimeSpan expirationTime)
		{
			this._baseCache.Insert(key, value, expirationTime);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E6 File Offset: 0x000002E6
		public void Insert(object key, object value, TimeSpan expirationTime, bool slidingExpiration)
		{
			this._baseCache.Insert(key, value, expirationTime, slidingExpiration);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F8 File Offset: 0x000002F8
		public void Insert(object key, object value, DateTime expirationDate, TimeSpan slidingExpirationTime)
		{
			this._baseCache.Insert(key, value, expirationDate, slidingExpirationTime);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000210A File Offset: 0x0000030A
		public void Remove(object key)
		{
			this._baseCache.Remove(key);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002118 File Offset: 0x00000318
		public void Remove(Predicate<object> pKey)
		{
			this._baseCache.Remove(pKey);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002126 File Offset: 0x00000326
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000213D File Offset: 0x0000033D
		public virtual Uri SasTokenProviderCloudServiceUri
		{
			get
			{
				return (Uri)this._baseCache["cSasTokenProviderCloudServiceUri"];
			}
			set
			{
				this._baseCache["cSasTokenProviderCloudServiceUri"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002179 File Offset: 0x00000379
		public virtual eAuthenticationMode AuthenticationMode
		{
			get
			{
				object obj = this._baseCache["AuthenticationMode"];
				if (obj == null)
				{
					return eAuthenticationMode.Single;
				}
				return (eAuthenticationMode)obj;
			}
			set
			{
				this._baseCache["AuthenticationMode"] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021F4 File Offset: 0x000003F4
		public virtual Token AuthenticationToken
		{
			get
			{
				eAuthenticationMode authenticationMode = this.AuthenticationMode;
				if (authenticationMode != eAuthenticationMode.Single)
				{
					if (authenticationMode == eAuthenticationMode.PerSession)
					{
						HttpContext httpContext = HttpContext.Current;
						if (((httpContext != null) ? httpContext.Session : null) != null)
						{
							return (Token)httpContext.Session["AuthenticationToken"];
						}
					}
					return null;
				}
				return (Token)this._baseCache["AuthenticationToken"];
			}
			set
			{
				eAuthenticationMode authenticationMode = this.AuthenticationMode;
				if (authenticationMode == eAuthenticationMode.Single)
				{
					this._baseCache["AuthenticationToken"] = value;
					return;
				}
				if (authenticationMode == eAuthenticationMode.PerSession)
				{
					HttpContext httpContext = HttpContext.Current;
					if (((httpContext != null) ? httpContext.Session : null) != null)
					{
						httpContext.Session["AuthenticationToken"] = value;
					}
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002248 File Offset: 0x00000448
		public virtual int WhoAmIId
		{
			get
			{
				eAuthenticationMode authenticationMode = this.AuthenticationMode;
				if (authenticationMode != eAuthenticationMode.Single)
				{
					if (authenticationMode == eAuthenticationMode.PerSession)
					{
						int whoAmIFromSessionCache = ClientCache.GetWhoAmIFromSessionCache();
						if (whoAmIFromSessionCache > 0)
						{
							return whoAmIFromSessionCache;
						}
					}
					return 0;
				}
				PersonBaseDTO personBaseDTO = (PersonBaseDTO)this._baseCache["cWhoAmI"];
				if (personBaseDTO == null)
				{
					return 0;
				}
				return personBaseDTO.PersonId;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002294 File Offset: 0x00000494
		private static int GetWhoAmIFromSessionCache()
		{
			HttpContext httpContext = HttpContext.Current;
			object obj;
			if (httpContext == null)
			{
				obj = null;
			}
			else
			{
				HttpSessionState session = httpContext.Session;
				obj = ((session != null) ? session["userinfo"] : null);
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				object value = obj2.GetType().GetProperty("ClockworkPid").GetValue(obj2, null);
				if (value is int)
				{
					return (int)value;
				}
			}
			object obj3;
			if (httpContext == null)
			{
				obj3 = null;
			}
			else
			{
				HttpSessionState session2 = httpContext.Session;
				obj3 = ((session2 != null) ? session2["identity"] : null);
			}
			obj2 = obj3;
			if (obj2 != null)
			{
				object value2 = obj2.GetType().GetProperty("PersonId").GetValue(obj2, null);
				if (value2 is int)
				{
					return (int)value2;
				}
			}
			return 0;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002338 File Offset: 0x00000538
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000234F File Offset: 0x0000054F
		[Obsolete("Use ObjectFactory.Resolve<ApplicationContext>() instead")]
		public virtual TechnoPro.Common.Public.Entities.ApplicationContext ApplicationContext
		{
			get
			{
				return (TechnoPro.Common.Public.Entities.ApplicationContext)this._baseCache["ApplicationContext"];
			}
			set
			{
				this._baseCache.Insert("ApplicationContext", value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002362 File Offset: 0x00000562
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002379 File Offset: 0x00000579
		public virtual string TenantId
		{
			get
			{
				return this._baseCache["TenantId"] as string;
			}
			set
			{
				this._baseCache.Insert("TenantId", value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023B9 File Offset: 0x000005B9
		public virtual string InstanceName
		{
			get
			{
				object obj = this._baseCache["instancename"];
				if (obj == null)
				{
					return "ClockWork";
				}
				return (string)obj;
			}
			set
			{
				this._baseCache.Insert("instancename", value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023F5 File Offset: 0x000005F5
		public virtual bool IsClockWorkServerEnable
		{
			get
			{
				object obj = this._baseCache["cClockWorkServerEnabled"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cClockWorkServerEnabled", value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000240D File Offset: 0x0000060D
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002424 File Offset: 0x00000624
		public virtual AcademicTermDTO CurrentAcademicTerm
		{
			get
			{
				return (AcademicTermDTO)this._baseCache["_currentAcademicTerm"];
			}
			set
			{
				if (value != null)
				{
					this._baseCache.Insert("_currentAcademicTerm", value.Clone(), TimeSpan.FromHours(8.0));
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000244D File Offset: 0x0000064D
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002464 File Offset: 0x00000664
		public virtual IList<AcademicTermDTO> AllAcademicTerms
		{
			get
			{
				return (IList<AcademicTermDTO>)this._baseCache["_allAcademicTerms"];
			}
			set
			{
				if (value != null)
				{
					this._baseCache.Insert("_allAcademicTerms", value.ToList<AcademicTermDTO>().ConvertAll<AcademicTermDTO>((AcademicTermDTO g) => g.Clone()), TimeSpan.FromHours(8.0));
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024BC File Offset: 0x000006BC
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000024D4 File Offset: 0x000006D4
		public virtual SessionDTO CurrentSession
		{
			get
			{
				return (SessionDTO)this._baseCache["_currentSession"];
			}
			set
			{
				if (value != null)
				{
					this._baseCache.Insert("_currentSession", value.Clone(), TimeSpan.FromHours(8.0));
					DateTime date = value.StartDate.Date;
					this._baseCache.Insert("cCurrentSessionStartDate", date, TimeSpan.FromHours(8.0));
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000253B File Offset: 0x0000073B
		public virtual DateTime? CurrentSessionStartDate
		{
			get
			{
				return (DateTime?)this._baseCache["cCurrentSessionStartDate"];
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002581 File Offset: 0x00000781
		public virtual string ServerCertificateString
		{
			get
			{
				object obj = this._baseCache["ServerCertificateString"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this._baseCache.Insert("ServerCertificateString", value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000025AB File Offset: 0x000007AB
		public virtual CertificateInfo ServerCertificateInfo
		{
			get
			{
				return (CertificateInfo)this._baseCache["ServerCertificateInfo"];
			}
			set
			{
				this._baseCache.Insert("ServerCertificateInfo", value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000025BE File Offset: 0x000007BE
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000025D5 File Offset: 0x000007D5
		public virtual InventoryCatalogDTO CurrentInventoryCatalog
		{
			get
			{
				return (InventoryCatalogDTO)this._baseCache["cInventoryCatalog"];
			}
			set
			{
				this._baseCache.Insert("cInventoryCatalog", value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025E8 File Offset: 0x000007E8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002611 File Offset: 0x00000811
		public virtual bool IsClient
		{
			get
			{
				object obj = this._baseCache["cIsClient"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cIsClient", value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002629 File Offset: 0x00000829
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002640 File Offset: 0x00000840
		public virtual DataTable screens
		{
			get
			{
				return (DataTable)this._baseCache["cScreens"];
			}
			set
			{
				this._baseCache.Insert("cScreens", value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002653 File Offset: 0x00000853
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002665 File Offset: 0x00000865
		public virtual object allScreens
		{
			get
			{
				return this._baseCache["cAllScreens"];
			}
			set
			{
				this._baseCache.Insert("cAllScreens", value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002678 File Offset: 0x00000878
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000268F File Offset: 0x0000088F
		public virtual IList<AppointmentRoomDTO> TestExamRoomsAll
		{
			get
			{
				return (IList<AppointmentRoomDTO>)this._baseCache["cTestExamRoomsAll"];
			}
			set
			{
				this._baseCache.Insert("cTestExamRoomsAll", value, TimeSpan.FromMinutes(60.0));
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026C7 File Offset: 0x000008C7
		public virtual IList<AppointmentRoomDTO> TestExamRoomsFinalExams
		{
			get
			{
				return (IList<AppointmentRoomDTO>)this._baseCache["cTestExamRoomsFinalExams"];
			}
			set
			{
				this._baseCache.Insert("cTestExamRoomsFinalExams", value, TimeSpan.FromMinutes(60.0));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000026FF File Offset: 0x000008FF
		public virtual IList<AppointmentRoomDTO> TestExamRoomsMidterms
		{
			get
			{
				return (IList<AppointmentRoomDTO>)this._baseCache["cTestExamRoomsMidterms"];
			}
			set
			{
				this._baseCache.Insert("cTestExamRoomsMidterms", value, TimeSpan.FromMinutes(60.0));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002720 File Offset: 0x00000920
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002749 File Offset: 0x00000949
		public virtual bool isAdmin
		{
			get
			{
				object obj = this._baseCache["cIsAdmin"];
				return obj != null && Convert.ToBoolean(obj);
			}
			set
			{
				this._baseCache.Insert("cIsAdmin", value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002761 File Offset: 0x00000961
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002778 File Offset: 0x00000978
		public virtual DataSet comboBoxData
		{
			get
			{
				return (DataSet)this._baseCache["cComboBoxData"];
			}
			set
			{
				this._baseCache.Insert("cComboBoxData", value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000278B File Offset: 0x0000098B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002793 File Offset: 0x00000993
		[Obsolete("Use Encryption instead")]
		public virtual IEncryption tripleDES
		{
			get
			{
				return this.Encryption;
			}
			set
			{
				this.Encryption = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000027B3 File Offset: 0x000009B3
		public virtual IEncryption Encryption
		{
			get
			{
				return (IEncryption)this._baseCache["cTripleDes"];
			}
			set
			{
				this._baseCache.Insert("cTripleDes", value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027C6 File Offset: 0x000009C6
		public virtual object tripleDESObj
		{
			get
			{
				return this._baseCache["cTripleDes"];
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000027EA File Offset: 0x000009EA
		public virtual object daObj
		{
			get
			{
				return this._baseCache["cDa"];
			}
			set
			{
				this._baseCache.Insert("cDa", value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027FD File Offset: 0x000009FD
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002814 File Offset: 0x00000A14
		public virtual DataTable staffNameTable
		{
			get
			{
				return (DataTable)this._baseCache["cStaffNameTable"];
			}
			set
			{
				this._baseCache.Insert("cStaffNameTable", value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002827 File Offset: 0x00000A27
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000283E File Offset: 0x00000A3E
		public virtual DataTable staff_noTable
		{
			get
			{
				return (DataTable)this._baseCache["cStaff_noTable"];
			}
			set
			{
				this._baseCache.Insert("cStaff_noTable", value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002851 File Offset: 0x00000A51
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002868 File Offset: 0x00000A68
		public virtual DataTable roomNameTable
		{
			get
			{
				return (DataTable)this._baseCache["cRoomNameTable"];
			}
			set
			{
				this._baseCache.Insert("cRoomNameTable", value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000287B File Offset: 0x00000A7B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002892 File Offset: 0x00000A92
		public virtual DataTable room_noTable
		{
			get
			{
				return (DataTable)this._baseCache["cRoom_noTable"];
			}
			set
			{
				this._baseCache.Insert("cRoom_noTable", value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028A5 File Offset: 0x00000AA5
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000028BC File Offset: 0x00000ABC
		public virtual DataTable resource_noTable
		{
			get
			{
				return (DataTable)this._baseCache["cResource_noTable"];
			}
			set
			{
				this._baseCache.Insert("cResource_noTable", value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028CF File Offset: 0x00000ACF
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000028E6 File Offset: 0x00000AE6
		public virtual DataTable resourceNameTable
		{
			get
			{
				return (DataTable)this._baseCache["cResourceNameTable"];
			}
			set
			{
				this._baseCache.Insert("cResourceNameTable", value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028F9 File Offset: 0x00000AF9
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002910 File Offset: 0x00000B10
		public virtual DataTable groupTable
		{
			get
			{
				return (DataTable)this._baseCache["cGroupTable"];
			}
			set
			{
				this._baseCache.Insert("cGroupTable", value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002923 File Offset: 0x00000B23
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000293A File Offset: 0x00000B3A
		public virtual DataSet lookupTablesForControls
		{
			get
			{
				return (DataSet)this._baseCache["cLookupTablesForControls"];
			}
			set
			{
				this._baseCache.Insert("cLookupTablesForControls", value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002950 File Offset: 0x00000B50
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002985 File Offset: 0x00000B85
		public virtual DateTime dtpNowAdjusted
		{
			get
			{
				object obj = this._baseCache["cDtpNowAdjusted"];
				if (obj == null)
				{
					return DateTime.Now.Date;
				}
				return (DateTime)obj;
			}
			set
			{
				this._baseCache.Insert("cDtpNowAdjusted", value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000299D File Offset: 0x00000B9D
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000029B4 File Offset: 0x00000BB4
		public virtual DataTable sessions
		{
			get
			{
				return (DataTable)this._baseCache["cSessions"];
			}
			set
			{
				this._baseCache.Insert("cSessions", value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000029C7 File Offset: 0x00000BC7
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000029DE File Offset: 0x00000BDE
		public virtual ArrayList eventHandlers
		{
			get
			{
				return (ArrayList)this._baseCache["cEventHandlersForDynamicForms"];
			}
			set
			{
				this._baseCache.Insert("cEventHandlersForDynamicForms", value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000029F1 File Offset: 0x00000BF1
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002A08 File Offset: 0x00000C08
		public virtual ArrayList Plugins
		{
			get
			{
				return (ArrayList)this._baseCache["cPlugins"];
			}
			set
			{
				this._baseCache.Insert("cPlugins", value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002A1B File Offset: 0x00000C1B
		public virtual DataTable ShowTimeAsTable
		{
			get
			{
				return (DataTable)this._baseCache["cShowTimeAsTable"];
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002A32 File Offset: 0x00000C32
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002A49 File Offset: 0x00000C49
		public virtual DataSet appLookupTables
		{
			get
			{
				return (DataSet)this._baseCache["cAppLookupTables"];
			}
			set
			{
				this._baseCache.Insert("cAppLookupTables", value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A5C File Offset: 0x00000C5C
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002A73 File Offset: 0x00000C73
		public virtual string TIME_FORMAT
		{
			get
			{
				return (string)this._baseCache["cTIME_FORMAT"];
			}
			set
			{
				this._baseCache.Insert("cTIME_FORMAT", value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A88 File Offset: 0x00000C88
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002ABA File Offset: 0x00000CBA
		public virtual string DATE_FORMAT
		{
			get
			{
				string text = (string)this._baseCache["cDATE_FORMAT"];
				if (string.IsNullOrEmpty(text))
				{
					return "yyyy-MM-dd";
				}
				return text;
			}
			set
			{
				this._baseCache.Insert("cDATE_FORMAT", value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002ACD File Offset: 0x00000CCD
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public virtual PersonBaseDTO currentStudent
		{
			get
			{
				return (PersonBaseDTO)this._baseCache["cCurrentStudent"];
			}
			set
			{
				this._baseCache.Insert("cCurrentStudent", value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002B25 File Offset: 0x00000D25
		public virtual string regionalLanguageCode
		{
			get
			{
				object obj = this._baseCache["cRegionalLanguageCode"];
				if (obj == null)
				{
					return "en-ca";
				}
				return (string)obj;
			}
			set
			{
				this._baseCache.Insert("cRegionalLanguageCode", value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002B38 File Offset: 0x00000D38
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002B4F File Offset: 0x00000D4F
		public virtual ImageList imageList3
		{
			get
			{
				return (ImageList)this._baseCache["cImageList3"];
			}
			set
			{
				this._baseCache.Insert("cImageList3", value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B62 File Offset: 0x00000D62
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002B79 File Offset: 0x00000D79
		public virtual ImageList imageList1
		{
			get
			{
				return (ImageList)this._baseCache["cImageList1"];
			}
			set
			{
				this._baseCache.Insert("cImageList1", value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002B8C File Offset: 0x00000D8C
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public virtual ImageList imageList2
		{
			get
			{
				return (ImageList)this._baseCache["cImageList2"];
			}
			set
			{
				this._baseCache.Insert("cImageList2", value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002BB6 File Offset: 0x00000DB6
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002BCD File Offset: 0x00000DCD
		public virtual ImageList iconsImageList
		{
			get
			{
				return (ImageList)this._baseCache["cIconsImageList"];
			}
			set
			{
				this._baseCache.Insert("cIconsImageList", value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002BE0 File Offset: 0x00000DE0
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002BF7 File Offset: 0x00000DF7
		public virtual DataTable iconInfo
		{
			get
			{
				return (DataTable)this._baseCache["cIconInfo"];
			}
			set
			{
				this._baseCache.Insert("cIconInfo", value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002C35 File Offset: 0x00000E35
		public virtual int emailControlID
		{
			get
			{
				object obj = this._baseCache["cEmailControlID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this._baseCache.Insert("cEmailControlID", value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002C50 File Offset: 0x00000E50
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002C79 File Offset: 0x00000E79
		public virtual int okToEmailControlID
		{
			get
			{
				object obj = this._baseCache["cOkToEmailControlID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this._baseCache.Insert("cOkToEmailControlID", value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002C94 File Offset: 0x00000E94
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002CCA File Offset: 0x00000ECA
		public virtual int ControlIDs_homePhone
		{
			get
			{
				int? num = (int?)this._baseCache["cControlIDs_homePhone"];
				if (num == null)
				{
					return 10;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this._baseCache.Insert("cControlIDs_homePhone", value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002D1A File Offset: 0x00000F1A
		public virtual int ControlIDs_workPhone
		{
			get
			{
				int? num = (int?)this._baseCache["cControlIDs_workPhone"];
				if (num == null)
				{
					return 11;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this._baseCache.Insert("cControlIDs_workPhone", value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002D34 File Offset: 0x00000F34
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002D69 File Offset: 0x00000F69
		public virtual int ControlIDs_male
		{
			get
			{
				int? num = (int?)this._baseCache["cControlIDs_male"];
				if (num == null)
				{
					return 4;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this._baseCache.Insert("cControlIDs_male", value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002D84 File Offset: 0x00000F84
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002DB9 File Offset: 0x00000FB9
		public virtual int ControlIDs_female
		{
			get
			{
				int? num = (int?)this._baseCache["cControlIDs_female"];
				if (num == null)
				{
					return 5;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this._baseCache.Insert("cControlIDs_female", value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002DD1 File Offset: 0x00000FD1
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public virtual int[] visibleAppTypeIds
		{
			get
			{
				return (int[])this._baseCache["cVisibleAppTypeIds"];
			}
			set
			{
				this._baseCache.Insert("cVisibleAppTypeIds", value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002DFB File Offset: 0x00000FFB
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002E12 File Offset: 0x00001012
		public virtual DataTable screensWithCids
		{
			get
			{
				return (DataTable)this._baseCache["cScreensWithCids"];
			}
			set
			{
				this._baseCache.Insert("cScreensWithCids", value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002E25 File Offset: 0x00001025
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002E3C File Offset: 0x0000103C
		public virtual int[][] studentGroupMemberships
		{
			get
			{
				return (int[][])this._baseCache["cStudentGroupMemberships"];
			}
			set
			{
				this._baseCache.Insert("cStudentGroupMemberships", value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002E50 File Offset: 0x00001050
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002E79 File Offset: 0x00001079
		public virtual bool prefersFrench
		{
			get
			{
				object obj = this._baseCache["cPrefersFrench"];
				return obj != null && Convert.ToBoolean(obj);
			}
			set
			{
				this._baseCache.Insert("cPrefersFrench", value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002E91 File Offset: 0x00001091
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002EA8 File Offset: 0x000010A8
		public virtual DataTable dynamicScreenNonDataControlsTable
		{
			get
			{
				return (DataTable)this._baseCache["cDynamicScreenNonDataControlsTable"];
			}
			set
			{
				this._baseCache.Insert("cDynamicScreenNonDataControlsTable", value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002EBB File Offset: 0x000010BB
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002ED2 File Offset: 0x000010D2
		public virtual DataTable instructorsTable
		{
			get
			{
				return (DataTable)this._baseCache["cInstructorsTable"];
			}
			set
			{
				this._baseCache.Insert("cInstructorsTable", value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002F1A File Offset: 0x0000111A
		public virtual int[] screenNumsAllowedToAdd
		{
			get
			{
				object obj = this._baseCache["cScreenNumsAllowedToAdd"];
				if (obj == null)
				{
					return new int[]
					{
						1
					};
				}
				return (int[])obj;
			}
			set
			{
				this._baseCache.Insert("cScreenNumsAllowedToAdd", value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002F2D File Offset: 0x0000112D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002F44 File Offset: 0x00001144
		public virtual Form chatterFrmUsers
		{
			get
			{
				return (Form)this._baseCache["cChatterFrmUsers"];
			}
			set
			{
				this._baseCache.Insert("cChatterFrmUsers", value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F58 File Offset: 0x00001158
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002F81 File Offset: 0x00001181
		public virtual int extraTimeType
		{
			get
			{
				object obj = this._baseCache["cExtraTimeType"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this._baseCache.Insert("cExtraTimeType", value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002F99 File Offset: 0x00001199
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002FB0 File Offset: 0x000011B0
		public virtual Image schedulerBackgroundImage
		{
			get
			{
				return (Image)this._baseCache["cSchedulerBackgroundImage"];
			}
			set
			{
				this._baseCache.Insert("cSchedulerBackgroundImage", value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002FED File Offset: 0x000011ED
		public virtual bool useActualStartTime
		{
			get
			{
				object obj = this._baseCache["cUseActualStartTime"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cUseActualStartTime", value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003008 File Offset: 0x00001208
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003031 File Offset: 0x00001231
		public virtual bool useSubTitle
		{
			get
			{
				object obj = this._baseCache["cUseSubTitle"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cUseSubTitle", value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000304C File Offset: 0x0000124C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000308C File Offset: 0x0000128C
		public virtual List<AppointmentDTO> copiedAppointments
		{
			get
			{
				object obj = this._baseCache["cCopiedAppointments"];
				if (obj == null)
				{
					List<AppointmentDTO> list = new List<AppointmentDTO>();
					this._baseCache.Insert("cCopiedAppointments", list);
					return list;
				}
				return (List<AppointmentDTO>)obj;
			}
			set
			{
				this._baseCache.Insert("cCopiedAppointments", value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000030BF File Offset: 0x000012BF
		public virtual string StudentCaption
		{
			get
			{
				return ((string)this._baseCache["cStudentCaption"]) ?? "";
			}
			set
			{
				this._baseCache.Insert("cStudentCaption", value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000030BF File Offset: 0x000012BF
		public virtual string DropListCaption_student
		{
			get
			{
				return ((string)this._baseCache["cStudentCaption"]) ?? "";
			}
			set
			{
				this._baseCache.Insert("cStudentCaption", value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000030D2 File Offset: 0x000012D2
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000030F2 File Offset: 0x000012F2
		public virtual string DropListCaption_staff
		{
			get
			{
				return ((string)this._baseCache["cStaffCaption"]) ?? "";
			}
			set
			{
				this._baseCache.Insert("cStaffCaption", value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003105 File Offset: 0x00001305
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003125 File Offset: 0x00001325
		public virtual string DropListCaption_resources
		{
			get
			{
				return ((string)this._baseCache["cResourcesCaption"]) ?? "";
			}
			set
			{
				this._baseCache.Insert("cResourcesCaption", value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003138 File Offset: 0x00001338
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003161 File Offset: 0x00001361
		public virtual bool AlwaysHideCurrentStudentNameFromStatusBar
		{
			get
			{
				object obj = this._baseCache["cAlwaysHideCurrentStudentNameFromStatusBar"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cAlwaysHideCurrentStudentNameFromStatusBar", value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000317C File Offset: 0x0000137C
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000031A9 File Offset: 0x000013A9
		public virtual string ungroupedAppTypeName
		{
			get
			{
				object obj = this._baseCache["cUngroupedAppTypeName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this._baseCache.Insert("cUngroupedAppTypeName", value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000031BC File Offset: 0x000013BC
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000031E5 File Offset: 0x000013E5
		public virtual bool showStudentGroupsOnSetActiveStudent
		{
			get
			{
				object obj = this._baseCache["cShowStudentGroupsOnSetActiveStudent"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._baseCache.Insert("cShowStudentGroupsOnSetActiveStudent", value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000031FD File Offset: 0x000013FD
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000320F File Offset: 0x0000140F
		public virtual object MainMDI
		{
			get
			{
				return this._baseCache["cMainMDI"];
			}
			set
			{
				this._baseCache.Insert("cMainMDI", value);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003224 File Offset: 0x00001424
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000324D File Offset: 0x0000144D
		public virtual int UsingCases
		{
			get
			{
				object obj = this._baseCache["cUsingCases"];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				this._baseCache["cUsingCases"] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003265 File Offset: 0x00001465
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000327C File Offset: 0x0000147C
		public virtual DataView cases
		{
			get
			{
				return (DataView)this._baseCache["cCases"];
			}
			set
			{
				this._baseCache.Insert("cCases", value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000328F File Offset: 0x0000148F
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000032A1 File Offset: 0x000014A1
		public virtual object emailSoftware
		{
			get
			{
				return this._baseCache["cEmailSoftware"];
			}
			set
			{
				this._baseCache.Insert("cEmailSoftware", value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000032B4 File Offset: 0x000014B4
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000032CB File Offset: 0x000014CB
		public virtual ClockWorkServerPreferredConnectionInfo ClientClockWorkServerConnectionInfo
		{
			get
			{
				return (ClockWorkServerPreferredConnectionInfo)this._baseCache["cClockWorkServerPreferredConnectionInfo"];
			}
			set
			{
				this._baseCache.Remove((object k) => k is string && (((string)k).EndsWith(".Endpoint") || ((string)k).EndsWith(".Binding")));
				this._baseCache.Insert("cClockWorkServerPreferredConnectionInfo", value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000331F File Offset: 0x0000151F
		public virtual ClockWorkServerPreferredConnectionInfo ClientClockWorkServerConnectionInfoOverride
		{
			get
			{
				return (ClockWorkServerPreferredConnectionInfo)this._baseCache["cClockWorkServerPreferredConnectionInfoOverride"];
			}
			set
			{
				this._baseCache.Remove((object k) => k is string && (((string)k).EndsWith(".Endpoint") || ((string)k).EndsWith(".Binding")));
				this._baseCache.Insert("cClockWorkServerPreferredConnectionInfoOverride", value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000335C File Offset: 0x0000155C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003373 File Offset: 0x00001573
		public virtual IList<IconInfoDTO> AppointmentIconInformation
		{
			get
			{
				return (IList<IconInfoDTO>)this._baseCache["cAppIconInfo"];
			}
			set
			{
				this._baseCache.Insert("cAppIconInfo", value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003386 File Offset: 0x00001586
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000339D File Offset: 0x0000159D
		public virtual IList<int> AllowedAppTypeIds
		{
			get
			{
				return (IList<int>)this._baseCache["cAllowedAppointmentTypeIds"];
			}
			set
			{
				this._baseCache.Insert("cAllowedAppointmentTypeIds", value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000033B0 File Offset: 0x000015B0
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000033C7 File Offset: 0x000015C7
		public virtual IList<InventoryLoanStatusDTO> LoanStatusList
		{
			get
			{
				return (IList<InventoryLoanStatusDTO>)this._baseCache["cInventoryLoanStatusList"];
			}
			set
			{
				this._baseCache.Insert("cInventoryLoanStatusList", value, TimeSpan.FromHours(8.0));
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000033E8 File Offset: 0x000015E8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000033FF File Offset: 0x000015FF
		public virtual IList<InventoryProductStatusDTO> ProductStatusList
		{
			get
			{
				return (IList<InventoryProductStatusDTO>)this._baseCache["cProductStatusList"];
			}
			set
			{
				this._baseCache.Insert("cProductStatusList", value, TimeSpan.FromHours(8.0));
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003449 File Offset: 0x00001649
		public virtual int mainMDIWidth
		{
			get
			{
				object obj = this._baseCache["cMainMDIWidth"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this._baseCache.Insert("cMainMDIWidth", value, TimeSpan.FromHours(8.0));
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000346F File Offset: 0x0000166F
		protected virtual bool IsUnitedStates
		{
			get
			{
				if (this._isUnitedStates == null)
				{
					this._isUnitedStates = new bool?(this.regionalLanguageCode != null && this.regionalLanguageCode == "en-us");
				}
				return this._isUnitedStates.Value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000034AF File Offset: 0x000016AF
		public virtual string DefaultDictionaryFile
		{
			get
			{
				if (this.IsUnitedStates)
				{
					return "en-US.dic";
				}
				return null;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000034C0 File Offset: 0x000016C0
		public virtual string Regional_invigilator_wording_with_a_or_an_preceding
		{
			get
			{
				if (!this.IsUnitedStates)
				{
					return "an invigilator";
				}
				return "a proctor";
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000034D5 File Offset: 0x000016D5
		public virtual string Regional_invigilator
		{
			get
			{
				if (!this.IsUnitedStates)
				{
					return "invigilator";
				}
				return "proctor";
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000034EA File Offset: 0x000016EA
		public virtual string Regional_invigilator_firstLetterCap
		{
			get
			{
				if (!this.IsUnitedStates)
				{
					return "Invigilator";
				}
				return "Proctor";
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000034FF File Offset: 0x000016FF
		public virtual string Regional_colour
		{
			get
			{
				if (!this.IsUnitedStates)
				{
					return "colour";
				}
				return "color";
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003514 File Offset: 0x00001714
		public virtual string Regional_cunsellor
		{
			get
			{
				if (!this.IsUnitedStates)
				{
					return "counsellor";
				}
				return "counselor";
			}
		}

		// Token: 0x04000002 RID: 2
		private readonly ICacheStorageManager _baseCache;

		// Token: 0x04000004 RID: 4
		private bool? _isUnitedStates;
	}
}
