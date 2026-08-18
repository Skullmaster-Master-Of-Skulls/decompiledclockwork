using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000DC RID: 220
	public class LegacyDynamicDataManager : ILegacyDynamicDataManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x00038965 File Offset: 0x00036B65
		public LegacyDynamicDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00038977 File Offset: 0x00036B77
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0003897F File Offset: 0x00036B7F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000869 RID: 2153 RVA: 0x00038988 File Offset: 0x00036B88
		private IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId, out eDynamicFormType formType)
		{
			ILegacyDynamicDataDAO legacyDynamicDataDAO = new LegacyDynamicDataDAO(this.OpContext);
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			DynamicForm dynamicForm = dynamicFormManager.LoadDynamicFormById(ScreenNum);
			bool flag = dynamicForm == null;
			if (flag)
			{
				throw new DatabaseSelectFailedException("LegacyDynamicDataManager:Failed to load form:Screennum=" + ScreenNum.ToString());
			}
			DynamicField dynamicField = dynamicFieldManager.LoadFieldByControlId(ControlId);
			bool flag2 = dynamicField == null;
			if (flag2)
			{
				throw new DatabaseSelectFailedException("LegacyDynamicDataManager:Failed to load control:ControlId=" + ControlId.ToString());
			}
			bool isDataEncrypted = dynamicField.IsDataEncrypted();
			formType = ((ScreenNum == 4) ? eDynamicFormType.Accommodation : dynamicForm.FormType);
			switch (formType)
			{
			case eDynamicFormType.PerStudent:
				return legacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItemsForPerStudentData(ControlId, isDataEncrypted);
			case eDynamicFormType.PerAppointment:
				return legacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItemsForPerAppointmentData(ControlId, isDataEncrypted);
			case eDynamicFormType.Accommodation:
			case eDynamicFormType.AccommodationTemplateOnly:
				return legacyDynamicDataDAO.GetDynamicDataDecryptedPreviewItemsForAccommodationData(ControlId, isDataEncrypted);
			}
			throw new InvalidParameterException("LegacyDynamicDataManager:InvalidFormTypeSpecified:ScreenNum=" + ScreenNum.ToString() + ":FormType=" + dynamicForm.FormType.ToString());
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00038AA8 File Offset: 0x00036CA8
		private static byte[] ExtractImageBytes(byte[] dbBytes, out string fileName)
		{
			byte[] result;
			try
			{
				byte[] array = new byte[6];
				for (int i = 0; i < 6; i++)
				{
					array[i] = dbBytes[i];
				}
				string s = LegacyDynamicDataManager.BytesToString(array);
				int num = int.Parse(s);
				byte[] array2 = new byte[num];
				for (int j = 0; j < num; j++)
				{
					array2[j] = dbBytes[j + 6];
				}
				string args = LegacyDynamicDataManager.BytesToString(array2);
				StringDictionary stringDictionary = LegacyDynamicDataManager.ParseArgs(args, ';');
				fileName = stringDictionary["filename"];
				string text = fileName;
				int num2 = dbBytes.Length - 6 - num;
				byte[] array3 = new byte[num2];
				for (int k = 0; k < array3.Length; k++)
				{
					array3[k] = dbBytes[k + num + 6];
				}
				result = array3;
			}
			catch (Exception ex)
			{
				fileName = "";
				result = new byte[0];
			}
			return result;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00038BA0 File Offset: 0x00036DA0
		private static string BytesToString(byte[] bytes)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(bytes);
			}
			return result;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00038BD0 File Offset: 0x00036DD0
		private static StringDictionary ParseArgs(string args, char delimiter)
		{
			return LegacyDynamicDataManager.ParseArgs(args, new char[]
			{
				delimiter
			});
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00038BF4 File Offset: 0x00036DF4
		private static StringDictionary ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						stringDictionary.Add(text, "");
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00038C88 File Offset: 0x00036E88
		public IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId)
		{
			eDynamicFormType eDynamicFormType;
			return this.GetDynamicDataDecryptedPreviewItems(ScreenNum, ControlId, out eDynamicFormType);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00038CA4 File Offset: 0x00036EA4
		public int ReverseEncryptionOnData(int ScreenNum, int ControlId, bool newEncrypted)
		{
			ILegacyDynamicDataDAO legacyDynamicDataDAO = new LegacyDynamicDataDAO(this.OpContext);
			eDynamicFormType eDynamicFormType;
			IList<DynamicDataDecryptedPreviewItem> dynamicDataDecryptedPreviewItems = this.GetDynamicDataDecryptedPreviewItems(ScreenNum, ControlId, out eDynamicFormType);
			switch (eDynamicFormType)
			{
			case eDynamicFormType.PerStudent:
				return newEncrypted ? legacyDynamicDataDAO.ReEncryptAndSaveDataPerStudent(dynamicDataDecryptedPreviewItems) : legacyDynamicDataDAO.ReDecryptAndSaveDataPerStudent(dynamicDataDecryptedPreviewItems);
			case eDynamicFormType.PerAppointment:
				return newEncrypted ? legacyDynamicDataDAO.ReEncryptAndSaveDataPerAppointment(dynamicDataDecryptedPreviewItems) : legacyDynamicDataDAO.ReDecryptAndSaveDataPerAppointment(dynamicDataDecryptedPreviewItems);
			case eDynamicFormType.Accommodation:
			case eDynamicFormType.AccommodationTemplateOnly:
				return newEncrypted ? legacyDynamicDataDAO.ReEncryptAndSaveDataAccommodationData(dynamicDataDecryptedPreviewItems) : legacyDynamicDataDAO.ReDecryptAndSaveDataAccommodationData(dynamicDataDecryptedPreviewItems);
			}
			throw new InvalidParameterException("LegacyDynamicDataManager:ReverseEncryptionOnData:formType not supported:formType=" + eDynamicFormType.ToString());
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00038D50 File Offset: 0x00036F50
		public string LookupStaffSignatureBase64(int pid)
		{
			ILegacyDynamicDataDAO legacyDynamicDataDAO = new LegacyDynamicDataDAO(this.OpContext);
			byte[] array = legacyDynamicDataDAO.LookupStaffSignature(pid);
			string text;
			return (array == null) ? "" : Convert.ToBase64String(LegacyDynamicDataManager.ExtractImageBytes(array, out text));
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00038D90 File Offset: 0x00036F90
		public void SaveLegacyStudentNote(LegacyStudentNote note)
		{
			ILegacyDynamicDataDAO legacyDynamicDataDAO = new LegacyDynamicDataDAO(this.OpContext);
			legacyDynamicDataDAO.SaveLegacyStudentNote(note);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00038DB4 File Offset: 0x00036FB4
		public IList<Pair<int, string>> GetPersonEmailPhone(int pid)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			throw new NotImplementedException();
		}
	}
}
