using System;
using System.Collections.Generic;
using System.IO;

namespace System.Web.Security.Cryptography
{
	// Token: 0x0200060E RID: 1550
	internal sealed class Purpose
	{
		// Token: 0x06004DD0 RID: 19920 RVA: 0x0010E32F File Offset: 0x0010C52F
		public Purpose(string primaryPurpose, params string[] specificPurposes) : this(primaryPurpose, specificPurposes, null, null)
		{
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0010E33B File Offset: 0x0010C53B
		internal Purpose(string primaryPurpose, string[] specificPurposes, CryptographicKey derivedEncryptionKey, CryptographicKey derivedValidationKey)
		{
			this.PrimaryPurpose = primaryPurpose;
			this.SpecificPurposes = (specificPurposes ?? new string[0]);
			this.DerivedEncryptionKey = derivedEncryptionKey;
			this.DerivedValidationKey = derivedValidationKey;
			this.SaveDerivedKeys = (this.SpecificPurposes.Length == 0);
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x0010E37A File Offset: 0x0010C57A
		// (set) Token: 0x06004DD3 RID: 19923 RVA: 0x0010E382 File Offset: 0x0010C582
		internal CryptographicKey DerivedEncryptionKey { get; private set; }

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x0010E38B File Offset: 0x0010C58B
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x0010E393 File Offset: 0x0010C593
		internal CryptographicKey DerivedValidationKey { get; private set; }

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x0010E39C File Offset: 0x0010C59C
		// (set) Token: 0x06004DD7 RID: 19927 RVA: 0x0010E3A4 File Offset: 0x0010C5A4
		internal bool SaveDerivedKeys { get; set; }

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0010E3B0 File Offset: 0x0010C5B0
		internal Purpose AppendSpecificPurpose(string specificPurpose)
		{
			string[] array = new string[this.SpecificPurposes.Length + 1];
			Array.Copy(this.SpecificPurposes, array, this.SpecificPurposes.Length);
			array[array.Length - 1] = specificPurpose;
			return new Purpose(this.PrimaryPurpose, array);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0010E3F8 File Offset: 0x0010C5F8
		internal Purpose AppendSpecificPurposes(IList<string> specificPurposes)
		{
			if (specificPurposes == null || specificPurposes.Count == 0)
			{
				return this;
			}
			string[] array = new string[this.SpecificPurposes.Length + specificPurposes.Count];
			Array.Copy(this.SpecificPurposes, array, this.SpecificPurposes.Length);
			specificPurposes.CopyTo(array, this.SpecificPurposes.Length);
			return new Purpose(this.PrimaryPurpose, array);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0010E458 File Offset: 0x0010C658
		public CryptographicKey GetDerivedEncryptionKey(IMasterKeyProvider masterKeyProvider, KeyDerivationFunction keyDerivationFunction)
		{
			CryptographicKey cryptographicKey = this.DerivedEncryptionKey;
			if (cryptographicKey == null)
			{
				CryptographicKey encryptionKey = masterKeyProvider.GetEncryptionKey();
				cryptographicKey = keyDerivationFunction(encryptionKey, this);
				if (this.SaveDerivedKeys)
				{
					this.DerivedEncryptionKey = cryptographicKey;
				}
			}
			return cryptographicKey;
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0010E490 File Offset: 0x0010C690
		public CryptographicKey GetDerivedValidationKey(IMasterKeyProvider masterKeyProvider, KeyDerivationFunction keyDerivationFunction)
		{
			CryptographicKey cryptographicKey = this.DerivedValidationKey;
			if (cryptographicKey == null)
			{
				CryptographicKey validationKey = masterKeyProvider.GetValidationKey();
				cryptographicKey = keyDerivationFunction(validationKey, this);
				if (this.SaveDerivedKeys)
				{
					this.DerivedValidationKey = cryptographicKey;
				}
			}
			return cryptographicKey;
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0010E4C8 File Offset: 0x0010C6C8
		internal void GetKeyDerivationParameters(out byte[] label, out byte[] context)
		{
			if (this._derivedKeyLabel == null)
			{
				this._derivedKeyLabel = CryptoUtil.SecureUTF8Encoding.GetBytes(this.PrimaryPurpose);
			}
			if (this._derivedKeyContext == null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, CryptoUtil.SecureUTF8Encoding))
					{
						foreach (string value in this.SpecificPurposes)
						{
							binaryWriter.Write(value);
						}
						this._derivedKeyContext = memoryStream.ToArray();
					}
				}
			}
			label = this._derivedKeyLabel;
			context = this._derivedKeyContext;
		}

		// Token: 0x04002982 RID: 10626
		public static readonly Purpose AnonymousIdentificationModule_Ticket = new Purpose("AnonymousIdentificationModule.Ticket", new string[0]);

		// Token: 0x04002983 RID: 10627
		public static readonly Purpose AssemblyResourceLoader_WebResourceUrl = new Purpose("AssemblyResourceLoader.WebResourceUrl", new string[0]);

		// Token: 0x04002984 RID: 10628
		public static readonly Purpose FormsAuthentication_Ticket = new Purpose("FormsAuthentication.Ticket", new string[0]);

		// Token: 0x04002985 RID: 10629
		public static readonly Purpose WebForms_Page_PreviousPageID = new Purpose("WebForms.Page.PreviousPageID", new string[0]);

		// Token: 0x04002986 RID: 10630
		public static readonly Purpose RolePrincipal_Ticket = new Purpose("RolePrincipal.Ticket", new string[0]);

		// Token: 0x04002987 RID: 10631
		public static readonly Purpose ScriptResourceHandler_ScriptResourceUrl = new Purpose("ScriptResourceHandler.ScriptResourceUrl", new string[0]);

		// Token: 0x04002988 RID: 10632
		public static readonly Purpose WebForms_ClientScriptManager_EventValidation = new Purpose("WebForms.ClientScriptManager.EventValidation", new string[0]);

		// Token: 0x04002989 RID: 10633
		public static readonly Purpose WebForms_DetailsView_KeyTable = new Purpose("WebForms.DetailsView.KeyTable", new string[0]);

		// Token: 0x0400298A RID: 10634
		public static readonly Purpose WebForms_GridView_DataKeys = new Purpose("WebForms.GridView.DataKeys", new string[0]);

		// Token: 0x0400298B RID: 10635
		public static readonly Purpose WebForms_GridView_SortExpression = new Purpose("WebForms.GridView.SortExpression", new string[0]);

		// Token: 0x0400298C RID: 10636
		public static readonly Purpose WebForms_HiddenFieldPageStatePersister_ClientState = new Purpose("WebForms.HiddenFieldPageStatePersister.ClientState", new string[0]);

		// Token: 0x0400298D RID: 10637
		public static readonly Purpose WebForms_ScriptManager_HistoryState = new Purpose("WebForms.ScriptManager.HistoryState", new string[0]);

		// Token: 0x0400298E RID: 10638
		public static readonly Purpose WebForms_SessionPageStatePersister_ClientState = new Purpose("WebForms.SessionPageStatePersister.ClientState", new string[0]);

		// Token: 0x0400298F RID: 10639
		public static readonly Purpose User_MachineKey_Protect = new Purpose("User.MachineKey.Protect", new string[0]);

		// Token: 0x04002990 RID: 10640
		public static readonly Purpose User_ObjectStateFormatter_Serialize = new Purpose("User.ObjectStateFormatter.Serialize", new string[0]);

		// Token: 0x04002991 RID: 10641
		public readonly string PrimaryPurpose;

		// Token: 0x04002992 RID: 10642
		public readonly string[] SpecificPurposes;

		// Token: 0x04002993 RID: 10643
		private byte[] _derivedKeyLabel;

		// Token: 0x04002994 RID: 10644
		private byte[] _derivedKeyContext;
	}
}
