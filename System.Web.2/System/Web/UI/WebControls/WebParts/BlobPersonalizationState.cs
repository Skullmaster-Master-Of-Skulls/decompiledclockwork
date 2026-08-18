using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052A RID: 1322
	internal sealed class BlobPersonalizationState : PersonalizationState
	{
		// Token: 0x060042EF RID: 17135 RVA: 0x000DB355 File Offset: 0x000D9555
		public BlobPersonalizationState(WebPartManager webPartManager) : base(webPartManager)
		{
			this._isPostRequest = (webPartManager.Page.Request.HttpVerb == HttpVerb.POST);
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x000DB377 File Offset: 0x000D9577
		public override bool IsEmpty
		{
			get
			{
				return this._extractedState == null || this._extractedState.Count == 0;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x000DB391 File Offset: 0x000D9591
		private bool IsPostRequest
		{
			get
			{
				return this._isPostRequest;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x000DB399 File Offset: 0x000D9599
		private PersonalizationScope PersonalizationScope
		{
			get
			{
				return base.WebPartManager.Personalization.Scope;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000DB3AB File Offset: 0x000D95AB
		private IDictionary SharedState
		{
			get
			{
				return this._sharedState;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x000DB3B3 File Offset: 0x000D95B3
		private IDictionary UserState
		{
			get
			{
				if (this._rawUserData != null)
				{
					this._userState = BlobPersonalizationState.DeserializeData(this._rawUserData);
					this._rawUserData = null;
				}
				if (this._userState == null)
				{
					this._userState = new HybridDictionary(false);
				}
				return this._userState;
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000DB3F0 File Offset: 0x000D95F0
		private void ApplyPersonalization(Control control, string personalizationID, bool isWebPartManager, PersonalizationScope extractScope, GenericWebPart genericWebPart)
		{
			if (this._personalizedControls == null)
			{
				this._personalizedControls = new HybridDictionary(false);
			}
			else if (this._personalizedControls.Contains(personalizationID))
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_CantApply", new object[]
				{
					personalizationID
				}));
			}
			IDictionary personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(control.GetType());
			if (this.SharedState == null)
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
			}
			BlobPersonalizationState.PersonalizationInfo personalizationInfo = (BlobPersonalizationState.PersonalizationInfo)this.SharedState[personalizationID];
			BlobPersonalizationState.PersonalizationInfo personalizationInfo2 = null;
			IDictionary defaultProperties = null;
			IDictionary initialProperties = null;
			PersonalizationDictionary customInitialProperties = null;
			BlobPersonalizationState.ControlInfo controlInfo = new BlobPersonalizationState.ControlInfo();
			controlInfo._allowSetDirty = false;
			this._personalizedControls[personalizationID] = controlInfo;
			if (personalizationInfo != null && personalizationInfo._isStatic && !personalizationInfo.IsMatchingControlType(control))
			{
				personalizationInfo = null;
				if (this.PersonalizationScope == PersonalizationScope.Shared)
				{
					this.SetControlDirty(control, personalizationID, isWebPartManager, true);
				}
			}
			IPersonalizable personalizable = control as IPersonalizable;
			ITrackingPersonalizable trackingPersonalizable = control as ITrackingPersonalizable;
			WebPart webPart = null;
			if (!isWebPartManager)
			{
				if (genericWebPart != null)
				{
					webPart = genericWebPart;
				}
				else
				{
					webPart = (WebPart)control;
				}
			}
			try
			{
				if (trackingPersonalizable != null)
				{
					trackingPersonalizable.BeginLoad();
				}
				if (this.PersonalizationScope == PersonalizationScope.User)
				{
					if (this.UserState == null)
					{
						throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
					}
					personalizationInfo2 = (BlobPersonalizationState.PersonalizationInfo)this.UserState[personalizationID];
					if (personalizationInfo2 != null && personalizationInfo2._isStatic && !personalizationInfo2.IsMatchingControlType(control))
					{
						personalizationInfo2 = null;
						this.SetControlDirty(control, personalizationID, isWebPartManager, true);
					}
					if (personalizable != null)
					{
						PersonalizationDictionary personalizationDictionary = this.MergeCustomProperties(personalizationInfo, personalizationInfo2, isWebPartManager, webPart, ref customInitialProperties);
						if (personalizationDictionary != null)
						{
							controlInfo._allowSetDirty = true;
							personalizable.Load(personalizationDictionary);
							controlInfo._allowSetDirty = false;
						}
					}
					if (!isWebPartManager)
					{
						IDictionary dictionary = null;
						IDictionary dictionary2 = null;
						if (personalizationInfo != null)
						{
							IDictionary properties = personalizationInfo._properties;
							if (properties != null && properties.Count != 0)
							{
								webPart.SetHasSharedData(true);
								dictionary = BlobPersonalizationState.SetPersonalizedProperties(control, personalizablePropertyEntries, properties, PersonalizationScope.Shared);
							}
						}
						defaultProperties = BlobPersonalizationState.GetPersonalizedProperties(control, personalizablePropertyEntries, null, null, extractScope);
						if (personalizationInfo2 != null)
						{
							IDictionary properties2 = personalizationInfo2._properties;
							if (properties2 != null && properties2.Count != 0)
							{
								webPart.SetHasUserData(true);
								dictionary2 = BlobPersonalizationState.SetPersonalizedProperties(control, personalizablePropertyEntries, properties2, extractScope);
							}
							if (trackingPersonalizable == null || !trackingPersonalizable.TracksChanges)
							{
								initialProperties = properties2;
							}
						}
						bool flag = dictionary != null || dictionary2 != null;
						if (flag)
						{
							IVersioningPersonalizable versioningPersonalizable = control as IVersioningPersonalizable;
							if (versioningPersonalizable != null)
							{
								IDictionary dictionary3 = null;
								if (dictionary != null)
								{
									dictionary3 = dictionary;
									if (dictionary2 == null)
									{
										goto IL_281;
									}
									using (IDictionaryEnumerator enumerator = dictionary2.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											object obj = enumerator.Current;
											DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
											dictionary3[dictionaryEntry.Key] = dictionaryEntry.Value;
										}
										goto IL_281;
									}
								}
								dictionary3 = dictionary2;
								IL_281:
								controlInfo._allowSetDirty = true;
								versioningPersonalizable.Load(dictionary3);
								controlInfo._allowSetDirty = false;
							}
							else
							{
								this.SetControlDirty(control, personalizationID, isWebPartManager, true);
							}
						}
					}
				}
				else
				{
					if (personalizable != null)
					{
						PersonalizationDictionary personalizationDictionary2 = this.MergeCustomProperties(personalizationInfo, personalizationInfo2, isWebPartManager, webPart, ref customInitialProperties);
						if (personalizationDictionary2 != null)
						{
							controlInfo._allowSetDirty = true;
							personalizable.Load(personalizationDictionary2);
							controlInfo._allowSetDirty = false;
						}
					}
					if (!isWebPartManager)
					{
						IDictionary dictionary4 = null;
						defaultProperties = BlobPersonalizationState.GetPersonalizedProperties(control, personalizablePropertyEntries, null, null, extractScope);
						if (personalizationInfo != null)
						{
							IDictionary properties3 = personalizationInfo._properties;
							if (properties3 != null && properties3.Count != 0)
							{
								webPart.SetHasSharedData(true);
								dictionary4 = BlobPersonalizationState.SetPersonalizedProperties(control, personalizablePropertyEntries, properties3, PersonalizationScope.Shared);
							}
							if (trackingPersonalizable == null || !trackingPersonalizable.TracksChanges)
							{
								initialProperties = properties3;
							}
						}
						if (dictionary4 != null)
						{
							IVersioningPersonalizable versioningPersonalizable2 = control as IVersioningPersonalizable;
							if (versioningPersonalizable2 != null)
							{
								controlInfo._allowSetDirty = true;
								versioningPersonalizable2.Load(dictionary4);
								controlInfo._allowSetDirty = false;
							}
							else
							{
								this.SetControlDirty(control, personalizationID, isWebPartManager, true);
							}
						}
					}
				}
			}
			finally
			{
				controlInfo._allowSetDirty = true;
				if (trackingPersonalizable != null)
				{
					trackingPersonalizable.EndLoad();
				}
			}
			controlInfo._control = control;
			controlInfo._personalizableProperties = personalizablePropertyEntries;
			controlInfo._defaultProperties = defaultProperties;
			controlInfo._initialProperties = initialProperties;
			controlInfo._customInitialProperties = customInitialProperties;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000DB7D8 File Offset: 0x000D99D8
		public override void ApplyWebPartPersonalization(WebPart webPart)
		{
			base.ValidateWebPart(webPart);
			if (webPart is UnauthorizedWebPart)
			{
				return;
			}
			string personalizationID = this.CreatePersonalizationID(webPart, null);
			PersonalizationScope personalizationScope = this.PersonalizationScope;
			if (personalizationScope == PersonalizationScope.User && !webPart.IsShared)
			{
				personalizationScope = PersonalizationScope.Shared;
			}
			this.ApplyPersonalization(webPart, personalizationID, false, personalizationScope, null);
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				Control childControl = genericWebPart.ChildControl;
				personalizationID = this.CreatePersonalizationID(childControl, genericWebPart);
				this.ApplyPersonalization(childControl, personalizationID, false, personalizationScope, genericWebPart);
			}
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000DB842 File Offset: 0x000D9A42
		public override void ApplyWebPartManagerPersonalization()
		{
			this.ApplyPersonalization(base.WebPartManager, "__wpm", true, this.PersonalizationScope, null);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000DB860 File Offset: 0x000D9A60
		private bool CompareProperties(IDictionary newProperties, IDictionary oldProperties)
		{
			int num = 0;
			int num2 = 0;
			if (newProperties != null)
			{
				num = newProperties.Count;
			}
			if (oldProperties != null)
			{
				num2 = oldProperties.Count;
			}
			if (num != num2)
			{
				return true;
			}
			if (num != 0)
			{
				foreach (object obj in newProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object key = dictionaryEntry.Key;
					object value = dictionaryEntry.Value;
					if (!oldProperties.Contains(key))
					{
						return true;
					}
					object objB = oldProperties[key];
					if (!object.Equals(value, objB))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000DB910 File Offset: 0x000D9B10
		private string CreatePersonalizationID(string ID, string genericWebPartID)
		{
			if (!string.IsNullOrEmpty(genericWebPartID))
			{
				return ID + "$" + genericWebPartID;
			}
			return ID;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000DB928 File Offset: 0x000D9B28
		private string CreatePersonalizationID(Control control, WebPart associatedGenericWebPart)
		{
			if (associatedGenericWebPart != null)
			{
				return this.CreatePersonalizationID(control.ID, associatedGenericWebPart.ID);
			}
			return this.CreatePersonalizationID(control.ID, null);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000DB950 File Offset: 0x000D9B50
		private static IDictionary DeserializeData(byte[] data)
		{
			IDictionary dictionary = null;
			if (data != null && data.Length != 0)
			{
				Exception ex = null;
				int num = -1;
				object[] array = null;
				int num2 = 0;
				try
				{
					ObjectStateFormatter objectStateFormatter = new ObjectStateFormatter(null, false);
					if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
					{
						HttpRuntime.NamedPermissionSet.PermitOnly();
					}
					array = (object[])objectStateFormatter.DeserializeWithAssert(new MemoryStream(data));
					if (array != null && array.Length != 0)
					{
						num = (int)array[num2++];
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (num == 1 || num == 2)
				{
					try
					{
						int num3 = (int)array[num2++];
						if (num3 > 0)
						{
							dictionary = new HybridDictionary(num3, false);
						}
						for (int i = 0; i < num3; i++)
						{
							Type type = null;
							VirtualPath controlVPath = null;
							object obj = array[num2++];
							string text;
							bool isStatic;
							if (obj is string)
							{
								text = (string)obj;
								isStatic = false;
							}
							else
							{
								type = (Type)obj;
								if (type == typeof(UserControl))
								{
									controlVPath = VirtualPath.CreateNonRelativeAllowNull((string)array[num2++]);
								}
								text = (string)array[num2++];
								isStatic = true;
							}
							IDictionary dictionary2 = null;
							int num4 = (int)array[num2++];
							if (num4 > 0)
							{
								dictionary2 = new HybridDictionary(num4, false);
								for (int j = 0; j < num4; j++)
								{
									string value = ((IndexedString)array[num2++]).Value;
									object value2 = array[num2++];
									dictionary2[value] = value2;
								}
							}
							PersonalizationDictionary personalizationDictionary = null;
							int num5 = (int)array[num2++];
							if (num5 > 0)
							{
								personalizationDictionary = new PersonalizationDictionary(num5);
								for (int k = 0; k < num5; k++)
								{
									string value3 = ((IndexedString)array[num2++]).Value;
									object value4 = array[num2++];
									PersonalizationScope scope = ((bool)array[num2++]) ? PersonalizationScope.Shared : PersonalizationScope.User;
									bool isSensitive = false;
									if (num == 2)
									{
										isSensitive = (bool)array[num2++];
									}
									personalizationDictionary[value3] = new PersonalizationEntry(value4, scope, isSensitive);
								}
							}
							dictionary[text] = new BlobPersonalizationState.PersonalizationInfo
							{
								_controlID = text,
								_controlType = type,
								_controlVPath = controlVPath,
								_isStatic = isStatic,
								_properties = dictionary2,
								_customProperties = personalizationDictionary
							};
						}
					}
					catch (Exception ex3)
					{
						ex = ex3;
					}
				}
				if (ex != null || (num != 1 && num != 2))
				{
					throw new ArgumentException(SR.GetString("BlobPersonalizationState_DeserializeError"), "data", ex);
				}
			}
			if (dictionary == null)
			{
				dictionary = new HybridDictionary(false);
			}
			return dictionary;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000DBC20 File Offset: 0x000D9E20
		private void ExtractPersonalization(Control control, string personalizationID, bool isWebPartManager, PersonalizationScope scope, bool isStatic, GenericWebPart genericWebPart)
		{
			if (this._extractedState == null)
			{
				this._extractedState = new HybridDictionary(false);
			}
			if (this._personalizedControls == null)
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotApplied"));
			}
			BlobPersonalizationState.ControlInfo controlInfo = (BlobPersonalizationState.ControlInfo)this._personalizedControls[personalizationID];
			if (controlInfo == null)
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_CantExtract", new object[]
				{
					personalizationID
				}));
			}
			ITrackingPersonalizable trackingPersonalizable = control as ITrackingPersonalizable;
			IPersonalizable personalizable = control as IPersonalizable;
			IDictionary dictionary = controlInfo._initialProperties;
			PersonalizationDictionary personalizationDictionary = controlInfo._customInitialProperties;
			bool flag = false;
			try
			{
				if (trackingPersonalizable != null)
				{
					trackingPersonalizable.BeginSave();
				}
				if (!this.IsPostRequest)
				{
					if (controlInfo._dirty)
					{
						if (personalizable != null)
						{
							PersonalizationDictionary personalizationDictionary2 = new PersonalizationDictionary();
							personalizable.Save(personalizationDictionary2);
							if (personalizationDictionary2.Count != 0 || (personalizationDictionary != null && personalizationDictionary.Count != 0))
							{
								if (scope == PersonalizationScope.User)
								{
									personalizationDictionary2.RemoveSharedProperties();
								}
								personalizationDictionary = personalizationDictionary2;
							}
						}
						if (!isWebPartManager)
						{
							dictionary = BlobPersonalizationState.GetPersonalizedProperties(control, controlInfo._personalizableProperties, controlInfo._defaultProperties, controlInfo._initialProperties, scope);
						}
						flag = true;
					}
				}
				else
				{
					bool flag2 = true;
					bool flag3 = true;
					if (controlInfo._dirty)
					{
						flag3 = false;
					}
					else if (trackingPersonalizable != null && trackingPersonalizable.TracksChanges && !controlInfo._dirty)
					{
						flag2 = false;
					}
					if (flag2)
					{
						if (personalizable != null && (controlInfo._dirty || personalizable.IsDirty))
						{
							PersonalizationDictionary personalizationDictionary3 = new PersonalizationDictionary();
							personalizable.Save(personalizationDictionary3);
							if (personalizationDictionary3.Count != 0 || (personalizationDictionary != null && personalizationDictionary.Count != 0))
							{
								if (personalizationDictionary3.Count != 0)
								{
									if (scope == PersonalizationScope.User)
									{
										personalizationDictionary3.RemoveSharedProperties();
									}
									personalizationDictionary = personalizationDictionary3;
								}
								else
								{
									personalizationDictionary = null;
								}
								flag3 = false;
								flag = true;
							}
						}
						if (!isWebPartManager)
						{
							IDictionary personalizedProperties = BlobPersonalizationState.GetPersonalizedProperties(control, controlInfo._personalizableProperties, controlInfo._defaultProperties, controlInfo._initialProperties, scope);
							if (flag3 && !this.CompareProperties(personalizedProperties, controlInfo._initialProperties))
							{
								flag2 = false;
							}
							if (flag2)
							{
								dictionary = personalizedProperties;
								flag = true;
							}
						}
					}
				}
			}
			finally
			{
				if (trackingPersonalizable != null)
				{
					trackingPersonalizable.EndSave();
				}
			}
			BlobPersonalizationState.PersonalizationInfo personalizationInfo = new BlobPersonalizationState.PersonalizationInfo();
			personalizationInfo._controlID = personalizationID;
			if (isStatic)
			{
				UserControl userControl = control as UserControl;
				if (userControl != null)
				{
					personalizationInfo._controlType = typeof(UserControl);
					personalizationInfo._controlVPath = userControl.TemplateControlVirtualPath;
				}
				else
				{
					personalizationInfo._controlType = control.GetType();
				}
			}
			personalizationInfo._isStatic = isStatic;
			personalizationInfo._properties = dictionary;
			personalizationInfo._customProperties = personalizationDictionary;
			this._extractedState[personalizationID] = personalizationInfo;
			if (flag)
			{
				base.SetDirty();
			}
			if ((dictionary != null && dictionary.Count > 0) || (personalizationDictionary != null && personalizationDictionary.Count > 0))
			{
				WebPart webPart = null;
				if (!isWebPartManager)
				{
					if (genericWebPart != null)
					{
						webPart = genericWebPart;
					}
					else
					{
						webPart = (WebPart)control;
					}
				}
				if (webPart != null)
				{
					if (this.PersonalizationScope == PersonalizationScope.Shared)
					{
						webPart.SetHasSharedData(true);
						return;
					}
					webPart.SetHasUserData(true);
				}
			}
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000DBEE8 File Offset: 0x000DA0E8
		public override void ExtractWebPartPersonalization(WebPart webPart)
		{
			base.ValidateWebPart(webPart);
			ProxyWebPart proxyWebPart = webPart as ProxyWebPart;
			if (proxyWebPart != null)
			{
				this.RoundTripWebPartPersonalization(proxyWebPart.OriginalID, proxyWebPart.GenericWebPartID);
				return;
			}
			PersonalizationScope personalizationScope = this.PersonalizationScope;
			if (personalizationScope == PersonalizationScope.User && !webPart.IsShared)
			{
				personalizationScope = PersonalizationScope.Shared;
			}
			bool isStatic = webPart.IsStatic;
			string personalizationID = this.CreatePersonalizationID(webPart, null);
			this.ExtractPersonalization(webPart, personalizationID, false, personalizationScope, isStatic, null);
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				Control childControl = genericWebPart.ChildControl;
				personalizationID = this.CreatePersonalizationID(childControl, genericWebPart);
				this.ExtractPersonalization(childControl, personalizationID, false, personalizationScope, isStatic, genericWebPart);
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000DBF77 File Offset: 0x000DA177
		public override void ExtractWebPartManagerPersonalization()
		{
			this.ExtractPersonalization(base.WebPartManager, "__wpm", true, this.PersonalizationScope, true, null);
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000DBF93 File Offset: 0x000DA193
		public override string GetAuthorizationFilter(string webPartID)
		{
			if (string.IsNullOrEmpty(webPartID))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("webPartID");
			}
			return this.GetPersonalizedValue(webPartID, "AuthorizationFilter") as string;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000DBFBC File Offset: 0x000DA1BC
		internal static IDictionary GetPersonalizedProperties(Control control, PersonalizationScope scope)
		{
			IDictionary personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(control.GetType());
			return BlobPersonalizationState.GetPersonalizedProperties(control, personalizablePropertyEntries, null, null, scope);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000DBFE0 File Offset: 0x000DA1E0
		private static IDictionary GetPersonalizedProperties(Control control, IDictionary personalizableProperties, IDictionary defaultPropertyState, IDictionary initialPropertyState, PersonalizationScope scope)
		{
			if (personalizableProperties.Count == 0)
			{
				return null;
			}
			bool flag = scope == PersonalizationScope.User;
			IDictionary dictionary = null;
			foreach (object obj in personalizableProperties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				PersonalizablePropertyEntry personalizablePropertyEntry = (PersonalizablePropertyEntry)dictionaryEntry.Value;
				if (!flag || personalizablePropertyEntry.Scope != PersonalizationScope.Shared)
				{
					PropertyInfo propertyInfo = personalizablePropertyEntry.PropertyInfo;
					string text = (string)dictionaryEntry.Key;
					object property = FastPropertyAccessor.GetProperty(control, text, control.DesignMode);
					bool flag2 = true;
					if ((initialPropertyState == null || !initialPropertyState.Contains(text)) && defaultPropertyState != null)
					{
						object objB = defaultPropertyState[text];
						if (object.Equals(property, objB))
						{
							flag2 = false;
						}
					}
					if (flag2)
					{
						if (dictionary == null)
						{
							dictionary = new HybridDictionary(personalizableProperties.Count, false);
						}
						dictionary[text] = property;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000DC0D4 File Offset: 0x000DA2D4
		private object GetPersonalizedValue(string personalizationID, string propertyName)
		{
			if (this.SharedState == null)
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
			}
			BlobPersonalizationState.PersonalizationInfo personalizationInfo = (BlobPersonalizationState.PersonalizationInfo)this.SharedState[personalizationID];
			IDictionary dictionary = (personalizationInfo != null) ? personalizationInfo._properties : null;
			if (this.PersonalizationScope == PersonalizationScope.Shared)
			{
				if (dictionary != null)
				{
					return dictionary[propertyName];
				}
			}
			else
			{
				if (this.UserState == null)
				{
					throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
				}
				BlobPersonalizationState.PersonalizationInfo personalizationInfo2 = (BlobPersonalizationState.PersonalizationInfo)this.UserState[personalizationID];
				IDictionary dictionary2 = (personalizationInfo2 != null) ? personalizationInfo2._properties : null;
				if (dictionary2 != null && dictionary2.Contains(propertyName))
				{
					return dictionary2[propertyName];
				}
				if (dictionary != null)
				{
					return dictionary[propertyName];
				}
			}
			return null;
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x000DC183 File Offset: 0x000DA383
		public void LoadDataBlobs(byte[] sharedData, byte[] userData)
		{
			this._sharedState = BlobPersonalizationState.DeserializeData(sharedData);
			this._rawUserData = userData;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000DC198 File Offset: 0x000DA398
		private PersonalizationDictionary MergeCustomProperties(BlobPersonalizationState.PersonalizationInfo sharedInfo, BlobPersonalizationState.PersonalizationInfo userInfo, bool isWebPartManager, WebPart hasDataWebPart, ref PersonalizationDictionary customInitialProperties)
		{
			PersonalizationDictionary personalizationDictionary = null;
			bool flag = sharedInfo != null && sharedInfo._customProperties != null;
			bool flag2 = userInfo != null && userInfo._customProperties != null;
			if (flag && flag2)
			{
				personalizationDictionary = new PersonalizationDictionary();
				foreach (object obj in sharedInfo._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					personalizationDictionary[(string)dictionaryEntry.Key] = (PersonalizationEntry)dictionaryEntry.Value;
				}
				using (IDictionaryEnumerator enumerator2 = userInfo._customProperties.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						personalizationDictionary[(string)dictionaryEntry2.Key] = (PersonalizationEntry)dictionaryEntry2.Value;
					}
					goto IL_F8;
				}
			}
			if (flag)
			{
				personalizationDictionary = sharedInfo._customProperties;
			}
			else if (flag2)
			{
				personalizationDictionary = userInfo._customProperties;
			}
			IL_F8:
			if (this.PersonalizationScope == PersonalizationScope.Shared && flag)
			{
				customInitialProperties = sharedInfo._customProperties;
			}
			else if (this.PersonalizationScope == PersonalizationScope.User && flag2)
			{
				customInitialProperties = userInfo._customProperties;
			}
			if (flag && !isWebPartManager)
			{
				hasDataWebPart.SetHasSharedData(true);
			}
			if (flag2 && !isWebPartManager)
			{
				hasDataWebPart.SetHasUserData(true);
			}
			return personalizationDictionary;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000DC304 File Offset: 0x000DA504
		private void RoundTripWebPartPersonalization(string ID, string genericWebPartID)
		{
			if (string.IsNullOrEmpty(ID))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("ID");
			}
			string personalizationID = this.CreatePersonalizationID(ID, genericWebPartID);
			this.RoundTripWebPartPersonalization(personalizationID);
			if (!string.IsNullOrEmpty(genericWebPartID))
			{
				string personalizationID2 = this.CreatePersonalizationID(genericWebPartID, null);
				this.RoundTripWebPartPersonalization(personalizationID2);
			}
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000DC34C File Offset: 0x000DA54C
		private void RoundTripWebPartPersonalization(string personalizationID)
		{
			if (this.PersonalizationScope == PersonalizationScope.Shared)
			{
				if (this.SharedState == null)
				{
					throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
				}
				if (this.SharedState.Contains(personalizationID))
				{
					this._extractedState[personalizationID] = (BlobPersonalizationState.PersonalizationInfo)this.SharedState[personalizationID];
					return;
				}
			}
			else
			{
				if (this.UserState == null)
				{
					throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotLoaded"));
				}
				if (this.UserState.Contains(personalizationID))
				{
					this._extractedState[personalizationID] = (BlobPersonalizationState.PersonalizationInfo)this.UserState[personalizationID];
				}
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000DC3E9 File Offset: 0x000DA5E9
		public byte[] SaveDataBlob()
		{
			return BlobPersonalizationState.SerializeData(this._extractedState);
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000DC3F8 File Offset: 0x000DA5F8
		private static byte[] SerializeData(IDictionary data)
		{
			byte[] result = null;
			if (data == null || data.Count == 0)
			{
				return result;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in data)
			{
				BlobPersonalizationState.PersonalizationInfo personalizationInfo = (BlobPersonalizationState.PersonalizationInfo)((DictionaryEntry)obj).Value;
				if ((personalizationInfo._properties != null && personalizationInfo._properties.Count != 0) || (personalizationInfo._customProperties != null && personalizationInfo._customProperties.Count != 0))
				{
					arrayList.Add(personalizationInfo);
				}
			}
			if (arrayList.Count != 0)
			{
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(2);
				arrayList2.Add(arrayList.Count);
				foreach (object obj2 in arrayList)
				{
					BlobPersonalizationState.PersonalizationInfo personalizationInfo2 = (BlobPersonalizationState.PersonalizationInfo)obj2;
					if (personalizationInfo2._isStatic)
					{
						arrayList2.Add(personalizationInfo2._controlType);
						if (personalizationInfo2._controlVPath != null)
						{
							arrayList2.Add(personalizationInfo2._controlVPath.AppRelativeVirtualPathString);
						}
					}
					arrayList2.Add(personalizationInfo2._controlID);
					int num = 0;
					if (personalizationInfo2._properties != null)
					{
						num = personalizationInfo2._properties.Count;
					}
					arrayList2.Add(num);
					if (num != 0)
					{
						foreach (object obj3 in personalizationInfo2._properties)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
							arrayList2.Add(new IndexedString((string)dictionaryEntry.Key));
							arrayList2.Add(dictionaryEntry.Value);
						}
					}
					int num2 = 0;
					if (personalizationInfo2._customProperties != null)
					{
						num2 = personalizationInfo2._customProperties.Count;
					}
					arrayList2.Add(num2);
					if (num2 != 0)
					{
						foreach (object obj4 in personalizationInfo2._customProperties)
						{
							DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj4;
							arrayList2.Add(new IndexedString((string)dictionaryEntry2.Key));
							PersonalizationEntry personalizationEntry = (PersonalizationEntry)dictionaryEntry2.Value;
							arrayList2.Add(personalizationEntry.Value);
							arrayList2.Add(personalizationEntry.Scope == PersonalizationScope.Shared);
							arrayList2.Add(personalizationEntry.IsSensitive);
						}
					}
				}
				if (arrayList2.Count != 0)
				{
					ObjectStateFormatter objectStateFormatter = new ObjectStateFormatter(null, false);
					MemoryStream memoryStream = new MemoryStream(1024);
					object[] stateGraph = arrayList2.ToArray();
					if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
					{
						HttpRuntime.NamedPermissionSet.PermitOnly();
					}
					objectStateFormatter.SerializeWithAssert(memoryStream, stateGraph);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000DC770 File Offset: 0x000DA970
		private void SetControlDirty(Control control, string personalizationID, bool isWebPartManager, bool forceSetDirty)
		{
			if (this._personalizedControls == null)
			{
				throw new InvalidOperationException(SR.GetString("BlobPersonalizationState_NotApplied"));
			}
			BlobPersonalizationState.ControlInfo controlInfo = (BlobPersonalizationState.ControlInfo)this._personalizedControls[personalizationID];
			if (controlInfo != null && (forceSetDirty || controlInfo._allowSetDirty))
			{
				controlInfo._dirty = true;
			}
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
		internal static IDictionary SetPersonalizedProperties(Control control, IDictionary propertyState)
		{
			IDictionary personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(control.GetType());
			return BlobPersonalizationState.SetPersonalizedProperties(control, personalizablePropertyEntries, propertyState, PersonalizationScope.Shared);
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
		private static IDictionary SetPersonalizedProperties(Control control, IDictionary personalizableProperties, IDictionary propertyState, PersonalizationScope scope)
		{
			if (personalizableProperties.Count == 0)
			{
				return propertyState;
			}
			if (propertyState == null || propertyState.Count == 0)
			{
				return null;
			}
			IDictionary dictionary = null;
			foreach (object obj in propertyState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				object value = dictionaryEntry.Value;
				PersonalizablePropertyEntry personalizablePropertyEntry = (PersonalizablePropertyEntry)personalizableProperties[text];
				bool flag = false;
				if (personalizablePropertyEntry != null && (scope == PersonalizationScope.Shared || personalizablePropertyEntry.Scope == PersonalizationScope.User))
				{
					PropertyInfo propertyInfo = personalizablePropertyEntry.PropertyInfo;
					try
					{
						FastPropertyAccessor.SetProperty(control, text, value, control.DesignMode);
						flag = true;
					}
					catch
					{
					}
				}
				if (!flag)
				{
					if (dictionary == null)
					{
						dictionary = new HybridDictionary(propertyState.Count, false);
					}
					dictionary[text] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000DC8D8 File Offset: 0x000DAAD8
		public override void SetWebPartDirty(WebPart webPart)
		{
			base.ValidateWebPart(webPart);
			string personalizationID = this.CreatePersonalizationID(webPart, null);
			this.SetControlDirty(webPart, personalizationID, false, false);
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				Control childControl = genericWebPart.ChildControl;
				personalizationID = this.CreatePersonalizationID(childControl, genericWebPart);
				this.SetControlDirty(childControl, personalizationID, false, false);
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000DC923 File Offset: 0x000DAB23
		public override void SetWebPartManagerDirty()
		{
			this.SetControlDirty(base.WebPartManager, "__wpm", true, false);
		}

		// Token: 0x040025B7 RID: 9655
		private const int PersonalizationVersion = 2;

		// Token: 0x040025B8 RID: 9656
		private const string WebPartManagerPersonalizationID = "__wpm";

		// Token: 0x040025B9 RID: 9657
		private bool _isPostRequest;

		// Token: 0x040025BA RID: 9658
		private IDictionary _personalizedControls;

		// Token: 0x040025BB RID: 9659
		private IDictionary _sharedState;

		// Token: 0x040025BC RID: 9660
		private IDictionary _userState;

		// Token: 0x040025BD RID: 9661
		private byte[] _rawUserData;

		// Token: 0x040025BE RID: 9662
		private IDictionary _extractedState;

		// Token: 0x020009E4 RID: 2532
		private sealed class PersonalizationInfo
		{
			// Token: 0x06006D0A RID: 27914 RVA: 0x0018675C File Offset: 0x0018495C
			public bool IsMatchingControlType(Control c)
			{
				if (c is ProxyWebPart)
				{
					return true;
				}
				if (this._controlType == null)
				{
					return false;
				}
				if (this._controlType == typeof(UserControl))
				{
					UserControl userControl = c as UserControl;
					return userControl != null && userControl.TemplateControlVirtualPath == this._controlVPath;
				}
				return this._controlType.IsAssignableFrom(c.GetType());
			}

			// Token: 0x04003A04 RID: 14852
			public Type _controlType;

			// Token: 0x04003A05 RID: 14853
			public VirtualPath _controlVPath;

			// Token: 0x04003A06 RID: 14854
			public string _controlID;

			// Token: 0x04003A07 RID: 14855
			public bool _isStatic;

			// Token: 0x04003A08 RID: 14856
			public IDictionary _properties;

			// Token: 0x04003A09 RID: 14857
			public PersonalizationDictionary _customProperties;
		}

		// Token: 0x020009E5 RID: 2533
		private sealed class ControlInfo
		{
			// Token: 0x04003A0A RID: 14858
			public Control _control;

			// Token: 0x04003A0B RID: 14859
			public IDictionary _personalizableProperties;

			// Token: 0x04003A0C RID: 14860
			public bool _dirty;

			// Token: 0x04003A0D RID: 14861
			public bool _allowSetDirty;

			// Token: 0x04003A0E RID: 14862
			public IDictionary _defaultProperties;

			// Token: 0x04003A0F RID: 14863
			public IDictionary _initialProperties;

			// Token: 0x04003A10 RID: 14864
			public PersonalizationDictionary _customInitialProperties;
		}

		// Token: 0x020009E6 RID: 2534
		private enum PersonalizationVersions
		{
			// Token: 0x04003A12 RID: 14866
			WhidbeyBeta2 = 1,
			// Token: 0x04003A13 RID: 14867
			WhidbeyRTM
		}
	}
}
