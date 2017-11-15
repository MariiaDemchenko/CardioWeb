using System;
using System.Collections.Generic;

namespace CardiologicalResearch.Models
{
    public interface IRepository
    {
        #region Users
        List<UserProfile> GetAllUsers();

        UserProfile GetUserByName(string name);

        UserProfile GetUserById(int id);
        #endregion

        #region Measurements
        List<Measurement> GetAllMeasurements();

        List<Measurement> GetAllUserMeasurements(int userId);

        List<Measurement> GetCurrentUserMeasurements(int userId);

        List<Measurement> GetMeasurementsByMedicalRecordId(int medicalRecordId);

        Measurement GetMeasurementById(int measurementId);

        Measurement AddMeasurement(Measurement measurement);

        Measurement AddMeasurement(int medicalRecordId, int parameterId, string value);

        Measurement EditMeasurement(Measurement measurement);

        Measurement EditMeasurement(int measurementId, int medicalRecordId, int parameterId, string value);

        void DeleteMeasurement(Measurement measurement);

        void DeleteMeasurement(int measurementId);
        #endregion

        #region MedicalRecords

        List<MedicalRecord> GetAllMedicalRecords();

        List<MedicalRecord> GetAllUserMedicalRecords(int userId);

        List<MedicalRecord> GetAllMedicalRecord2Users();

        MedicalRecord GetCurrentUserMedicalRecord(int userId);

        MedicalRecord GetLastMedicalRecord();

        MedicalRecord GetMedicalRecordById(int id);

        MedicalRecord AddUserMedicalRecord(int userId);

        MedicalRecord AddMedicalRecord(MedicalRecord medicalRecord);

        MedicalRecord AddMedicalRecord(int userId, DateTime medicalRecordDate);

        MedicalRecord EditMedicalRecord(MedicalRecord medicalRecord);

        MedicalRecord EditMedicalRecord(int medicalRecordId, int userId, DateTime medicalRecordDate);

        MedicalRecord DeleteMedicalRecord(MedicalRecord medicalRecord);

        void DeleteMedicalRecord(int medicalRecordId);
        #endregion

        #region Examination

        List<Examination> GetAllExaminations();

        List<Examination> GetAllExamination2MedicalRecords();

        Examination GetCurrentUserExamination(int userId);

        Examination GetExaminationById(int examinationId);

        Examination AddExamination(Examination examination);

        Examination AddExamination(int medicalRecordId, DateTime examinationDate);

        Examination EditExamination(Examination examination);

        Examination EditExamination(int examinationId, int medicalRecordId, DateTime examinationDate);

        Examination DeleteExamination(Examination examination);

        void DeleteExamination(int examinationId);
        #endregion

        #region ExaminationResult

        List<ExaminationResult> GetAllExaminationResults();

        List<ExaminationResult> GetCurrentUserExaminationResults(int userId);

        List<ExaminationResult> GetExaminationResultsByExaminationId(int examinationId);

        ExaminationResult GetExaminationResultById(int examinationResultId);

        ExaminationResult AddUserExaminationResult(ExaminationResult examinationResult);

        ExaminationResult EditUserExaminationResult(ExaminationResult examinationResult);

        ExaminationResult DeleteExaminationResult(ExaminationResult examinationResult);
        #endregion
    }
}
