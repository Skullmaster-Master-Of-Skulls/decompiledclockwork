using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Telerik.Pdf.Security
{
	// Token: 0x02001679 RID: 5753
	public class SecurityManager
	{
		// Token: 0x0600DE6C RID: 56940 RVA: 0x00309687 File Offset: 0x00307887
		public SecurityManager(SecurityOptions options, FileIdentifier fileId)
		{
			this.CreateOwnerEntry(options);
			this.CreateMasterKey(options, fileId);
			this.CreateUserEntry(options);
			this.permissions = options.Permissions;
		}

		// Token: 0x0600DE6D RID: 56941 RVA: 0x003096B4 File Offset: 0x003078B4
		public PdfDictionary GetEncrypt(PdfObjectId objectId)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(objectId);
			pdfDictionary[PdfName.Names.Filter] = PdfName.Names.Standard;
			pdfDictionary[PdfName.Names.V] = new PdfNumeric(1m);
			pdfDictionary[PdfName.Names.Length] = new PdfNumeric(40m);
			pdfDictionary[PdfName.Names.R] = new PdfNumeric(2m);
			PdfString pdfString = new PdfString(this.ownerEntry);
			pdfString.NeverEncrypt = true;
			pdfDictionary[PdfName.Names.O] = pdfString;
			PdfString pdfString2 = new PdfString(this.userEntry);
			pdfString2.NeverEncrypt = true;
			pdfDictionary[PdfName.Names.U] = pdfString2;
			pdfDictionary[PdfName.Names.P] = new PdfNumeric(this.permissions);
			return pdfDictionary;
		}

		// Token: 0x0600DE6E RID: 56942 RVA: 0x00309775 File Offset: 0x00307975
		private void CreateMasterKey(SecurityOptions options, FileIdentifier fileId)
		{
			this.masterKey = SecurityManager.ComputeEncryptionKey32(SecurityManager.PadPassword(options.UserPassword), this.ownerEntry, options.Permissions, fileId.CreatedPart);
		}

		// Token: 0x0600DE6F RID: 56943 RVA: 0x003097A0 File Offset: 0x003079A0
		private void CreateOwnerEntry(SecurityOptions options)
		{
			string text = options.OwnerPassword;
			if (text == null)
			{
				text = options.UserPassword;
			}
			byte[] buffer = SecurityManager.PadPassword(text);
			MD5 md = MD5.Create();
			byte[] key = md.ComputeHash(buffer);
			byte[] dataIn = SecurityManager.PadPassword(options.UserPassword);
			Arc4 arc = new Arc4(key, 0, 5);
			this.ownerEntry = new byte[32];
			arc.Encrypt(dataIn, this.ownerEntry);
		}

		// Token: 0x0600DE70 RID: 56944 RVA: 0x00309808 File Offset: 0x00307A08
		private void CreateUserEntry(SecurityOptions options)
		{
			Arc4 arc = new Arc4(this.masterKey);
			this.userEntry = new byte[32];
			arc.Encrypt(SecurityManager.Padding, this.userEntry);
		}

		// Token: 0x0600DE71 RID: 56945 RVA: 0x00309840 File Offset: 0x00307A40
		public byte[] Encrypt(byte[] data, PdfObjectId objectId)
		{
			Arc4 arc = new Arc4(SecurityManager.ComputeEncryptionKey31(this.masterKey, objectId));
			arc.Encrypt(data, data);
			return data;
		}

		// Token: 0x17004409 RID: 17417
		// (get) Token: 0x0600DE72 RID: 56946 RVA: 0x00309868 File Offset: 0x00307A68
		// (set) Token: 0x0600DE73 RID: 56947 RVA: 0x00309870 File Offset: 0x00307A70
		internal byte[] UserEntry
		{
			get
			{
				return this.userEntry;
			}
			set
			{
				this.userEntry = value;
			}
		}

		// Token: 0x1700440A RID: 17418
		// (get) Token: 0x0600DE74 RID: 56948 RVA: 0x00309879 File Offset: 0x00307A79
		// (set) Token: 0x0600DE75 RID: 56949 RVA: 0x00309881 File Offset: 0x00307A81
		internal byte[] OwnerEntry
		{
			get
			{
				return this.ownerEntry;
			}
			set
			{
				this.ownerEntry = value;
			}
		}

		// Token: 0x0600DE76 RID: 56950 RVA: 0x0030988C File Offset: 0x00307A8C
		private static byte[] ComputeEncryptionKey31(byte[] masterKey, PdfObjectId objectId)
		{
			byte[] array = new byte[masterKey.Length + 5];
			Array.Copy(masterKey, 0, array, 0, masterKey.Length);
			int num = masterKey.Length;
			array[num++] = (byte)(objectId.ObjectNumber & 255);
			array[num++] = (byte)(objectId.ObjectNumber >> 8 & 255);
			array[num++] = (byte)(objectId.ObjectNumber >> 16 & 255);
			array[num++] = (byte)(objectId.GenerationNumber & 255);
			array[num++] = (byte)(objectId.GenerationNumber >> 8 & 255);
			MD5 md = MD5.Create();
			byte[] sourceArray = md.ComputeHash(array);
			Array.Copy(sourceArray, 0, array, 0, masterKey.Length + 5);
			return array;
		}

		// Token: 0x0600DE77 RID: 56951 RVA: 0x00309944 File Offset: 0x00307B44
		private static byte[] ComputeEncryptionKey32(byte[] paddedPassword, byte[] ownerEntry, int permissions, byte[] fileId)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(paddedPassword, 0, 32);
			memoryStream.Write(ownerEntry, 0, 32);
			memoryStream.Write(BitConverter.GetBytes(permissions), 0, 4);
			memoryStream.Write(fileId, 0, fileId.Length);
			MD5 md = MD5.Create();
			byte[] sourceArray = md.ComputeHash(memoryStream.ToArray());
			byte[] array = new byte[5];
			Array.Copy(sourceArray, 0, array, 0, 5);
			return array;
		}

		// Token: 0x0600DE78 RID: 56952 RVA: 0x003099AC File Offset: 0x00307BAC
		private static byte[] PadPassword(string password)
		{
			byte[] array = new byte[32];
			if (password != null)
			{
				int num = (password.Length < 32) ? password.Length : 32;
				Encoding.ASCII.GetBytes(password, 0, num, array, 0);
				int i = num;
				int num2 = 0;
				while (i < 32)
				{
					array[i] = SecurityManager.Padding[num2];
					i++;
					num2++;
				}
			}
			else
			{
				SecurityManager.Padding.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x0600DE79 RID: 56953 RVA: 0x00309A15 File Offset: 0x00307C15
		internal static bool CheckUserPassword(string password, byte[] userEntry, byte[] ownerEntry, int permissions, byte[] fileId)
		{
			return SecurityManager.CheckUserPassword(SecurityManager.PadPassword(password), userEntry, ownerEntry, permissions, fileId);
		}

		// Token: 0x0600DE7A RID: 56954 RVA: 0x00309A28 File Offset: 0x00307C28
		private static bool CheckUserPassword(byte[] paddedPassword, byte[] userEntry, byte[] ownerEntry, int permissions, byte[] fileId)
		{
			byte[] key = SecurityManager.ComputeEncryptionKey32(paddedPassword, ownerEntry, permissions, fileId);
			Arc4 arc = new Arc4(key);
			byte[] array = new byte[32];
			arc.Encrypt(userEntry, array);
			return SecurityManager.CompareArray(SecurityManager.Padding, array);
		}

		// Token: 0x0600DE7B RID: 56955 RVA: 0x00309A64 File Offset: 0x00307C64
		internal static bool CheckOwnerPassword(string password, byte[] userEntry, byte[] ownerEntry, int permissions, byte[] fileId)
		{
			MD5 md = MD5.Create();
			byte[] key = md.ComputeHash(SecurityManager.PadPassword(password));
			Arc4 arc = new Arc4(key, 0, 5);
			byte[] array = new byte[32];
			arc.Encrypt(ownerEntry, array);
			return SecurityManager.CheckUserPassword(array, userEntry, ownerEntry, permissions, fileId);
		}

		// Token: 0x0600DE7C RID: 56956 RVA: 0x00309AA8 File Offset: 0x00307CA8
		private static bool CompareArray(byte[] a1, byte[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003FF9 RID: 16377
		private static readonly byte[] Padding = new byte[]
		{
			40,
			191,
			78,
			94,
			78,
			117,
			138,
			65,
			100,
			0,
			78,
			86,
			byte.MaxValue,
			250,
			1,
			8,
			46,
			46,
			0,
			182,
			208,
			104,
			62,
			128,
			47,
			12,
			169,
			254,
			100,
			83,
			105,
			122
		};

		// Token: 0x04003FFA RID: 16378
		private byte[] ownerEntry;

		// Token: 0x04003FFB RID: 16379
		private byte[] userEntry;

		// Token: 0x04003FFC RID: 16380
		private byte[] masterKey;

		// Token: 0x04003FFD RID: 16381
		private int permissions;
	}
}
