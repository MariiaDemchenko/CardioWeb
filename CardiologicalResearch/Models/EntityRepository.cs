using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CardiologicalResearch.Models
{
    public class EntityRepository : IRepository
    {

        #region Users

        public List<UserProfile> GetAllUsers()
        {
            List<UserProfile> users = null;
            try
            {
                using (var context = new CardioContext())
                {
                    users = context.UserProfiles.AsNoTracking().ToList();
                }
            }
            catch (Exception e)
            {
                var z = e;
            }

            return users;
        }

        public UserProfile GetUserByName(string name)
        {
            UserProfile user = null;
            try
            {
                using (var context = new CardioContext())
                {
                    user = context.UserProfiles.Where(x => x.UserName == name).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var z = e;
            }

            return user;
        }

        public UserProfile GetUserById(int id)
        {
            UserProfile user = null;
            try
            {
                using (var context = new CardioContext())
                {
                    user = context.UserProfiles.FirstOrDefault(x => x.UserId == id);
                }
            }
            catch (Exception e)
            {
                var z = e;
            }

            return user;
        }
        #endregion

        #region Measurements

        public List<Measurement> GetAllUserMeasurements(int userId)
        {
            List<Measurement> measurements;
            using (var db = new CardioContext())
            {
                var userMedicalRecords = db.MedicalRecords.Where(p => p.UserId == userId)
                    .Select(m => m.MedicalRecordId).ToList();

                measurements = db.Measurements.Where(p => userMedicalRecords.Contains(p.MedicalRecordId)).ToList();
            }
            return measurements;
        }

        public List<Measurement> GetCurrentUserMeasurements(int userId)
        {
            var measurements = new List<Measurement>();
            using (var db = new CardioContext())
            {
                var userCurrentMedicalRecordId = GetCurrentUserMedicalRecord(userId)?.MedicalRecordId;

                if (userCurrentMedicalRecordId != null)
                {
                    measurements = db.Measurements.Where(p => p.MedicalRecordId == userCurrentMedicalRecordId).ToList();
                }
            }
            return measurements;
        }

        public List<Measurement> GetMeasurementsByMedicalRecordId(int medicalRecordId)
        {
            List<Measurement> measurements;
            using (var db = new CardioContext())
            {
                measurements = db.Measurements.Where(d => d.MedicalRecordId == medicalRecordId).ToList();
            }
            return measurements;
        }

        public List<Measurement> GetAllMeasurements()
        {
            List<Measurement> measurements;
            using (var db = new CardioContext())
            {
                measurements = db.Measurements.ToList();
            }
            return measurements;
        }

        public Measurement EditMeasurement(Measurement measurement)
        {
            using (var db = new CardioContext())
            {
                db.Entry(measurement).State = EntityState.Modified;
                db.SaveChanges();
            }
            return measurement;
        }

        public Measurement EditMeasurement(int measurementId, int medicalRecordId, int parameterId, string value)
        {
            var measurement = GetMeasurementById(measurementId);
            measurement.ParameterId = (ParameterType) parameterId;
            measurement.MedicalRecordId = medicalRecordId;
            measurement.Value = value;

            EditMeasurement(measurement);

            return measurement;
        }

        public void DeleteMeasurement(Measurement measurement)
        {
            using (var db = new CardioContext())
            {
                db.Measurements.Remove(measurement);
                db.SaveChanges();
            }
            return;
        }

        public void DeleteMeasurement(int measurementId)
        {
            var measurement = GetMeasurementById(measurementId);

            using (var db = new CardioContext())
            {
                db.Measurements.Attach(measurement);
                db.Measurements.Remove(measurement);
                db.SaveChanges();
            }
            return;
        }

        public Measurement GetMeasurementById(int measurementId)
        {
            var measurement = new Measurement();
            using (var db = new CardioContext())
            {
                measurement = db.Measurements.Find(measurementId);
            }
            return measurement;
        }

        public Measurement AddMeasurement(Measurement measurement)
        {
            using (var db = new CardioContext())
            {
                db.Measurements.Add(measurement);
                db.SaveChanges();
            }
            return measurement;
        }

        public Measurement AddMeasurement(int medicalRecordId, int parameterId, string value)
        {
            var measurement = new Measurement()
            {
                MedicalRecordId = medicalRecordId,
                ParameterId = (ParameterType) parameterId,
                Value = value
            };

            AddMeasurement(measurement);
            return measurement;
        }
        #endregion

        #region Examinations

        public Examination GetCurrentUserExamination(int userId)
        {
            var examination = new Examination();
            using (var db = new CardioContext())
            {
                var userCurrentMedicalRecordId = GetCurrentUserMedicalRecord(userId)?.MedicalRecordId;

                if (userCurrentMedicalRecordId != null)
                {
                    examination = db.Examinations
                        .Where(p => p.MedicalRecordId == userCurrentMedicalRecordId)
                        .OrderByDescending(p => p.ExaminationId).FirstOrDefault();
                }
            }
            return examination;
        }

        public Examination AddExamination(Examination examination)
        {
            using (var db = new CardioContext())
            {
                db.Examinations.Add(examination);
                db.SaveChanges();
            }
            return examination;
        }

        public Examination AddExamination(int medicalRecordId, DateTime examinationDate)
        {
            var examination = new Examination
            {
                MedicalRecordId = medicalRecordId,
                ExaminationDate = examinationDate
            };

            AddExamination(examination);
            return examination;
        }

        public Examination GetExaminationById(int examinationId)
        {
            Examination examination;
            using (var db = new CardioContext())
            {
                examination = db.Examinations.FirstOrDefault(e => e.ExaminationId == examinationId);
            }
            return examination;
        }

        public List<Examination> GetAllExaminations()
        {
            List<Examination> examinations;
            using (var db = new CardioContext())
            {
                examinations = db.Examinations.ToList();
            }
            return examinations;
        }

        public List<Examination> GetAllExamination2MedicalRecords()
        {
            List<Examination> examinations;
            using (var db = new CardioContext())
            {
                examinations = db.Examinations.Include(e=>e.MedicalRecord).ToList();
            }
            return examinations;
        }

        public Examination EditExamination(Examination examination)
        {
            using (var db = new CardioContext())
            {
                db.Entry(examination).State = EntityState.Modified;
                db.SaveChanges();
            }
            return examination;
        }

        public Examination EditExamination(int examinationId, int medicalRecordId, DateTime examinationDate)
        {
            var examination = GetExaminationById(examinationId);
            examination.ExaminationDate = examinationDate;
            examination.MedicalRecordId = medicalRecordId;
            EditExamination(examination);
            return examination;
        }

        public Examination DeleteExamination(Examination examination)
        {
            using (var db = new CardioContext())
            {
                db.Examinations.Remove(examination);
                db.SaveChanges();
            }
            return examination;
        }

        public void DeleteExamination(int examinationId)
        {
            var examination = GetExaminationById(examinationId);

            using (var db = new CardioContext())
            {
                db.Examinations.Attach(examination);
                db.Examinations.Remove(examination);
                db.SaveChanges();
            }
            return;
        }
        #endregion

        #region ExaminationResults

        public List<ExaminationResult> GetCurrentUserExaminationResults(int userId)
        {
            var examinationResults = new List<ExaminationResult>();
            using (var db = new CardioContext())
            {
                var userCurrentExaminationId = GetCurrentUserExamination(userId)?.ExaminationId;

                if (userCurrentExaminationId != null)
                {
                    examinationResults = db.ExaminationResults
                        .Where(p => p.ExaminationId == userCurrentExaminationId)
                        .OrderByDescending(p => p.ExaminationId).ToList();
                }
            }
            return examinationResults;
        }

        public ExaminationResult GetExaminationResultById(int examinationResultId)
        {
            ExaminationResult examinationResult;
            using (var db = new CardioContext())
            {
                examinationResult = db.ExaminationResults.Find(examinationResultId);
            }
            return examinationResult;
        }

        public List<ExaminationResult> GetExaminationResultsByExaminationId(int examinationId)
        {
            List<ExaminationResult> examinationResults;
            using (var db = new CardioContext())
            {
                examinationResults = db.ExaminationResults.Where(p => p.ExaminationId == examinationId).ToList();
            }
            return examinationResults;
        }

        public ExaminationResult AddUserExaminationResult(ExaminationResult examinationResult)
        {
            using (var db = new CardioContext())
            {
                db.ExaminationResults.Add(examinationResult);
                db.SaveChanges();
            }
            return examinationResult;
        }

        public ExaminationResult EditUserExaminationResult(ExaminationResult examinationResult)
        {
            using (var db = new CardioContext())
            {
                db.Entry(examinationResult).State = EntityState.Modified;
                db.SaveChanges();
            }
            return examinationResult;
        }

        public List<ExaminationResult> GetAllExaminationResults()
        {
            List<ExaminationResult> examinationResults;
            using (var db = new CardioContext())
            {
                examinationResults = db.ExaminationResults.ToList();
            }
            return examinationResults;
        }

        public ExaminationResult DeleteExaminationResult(ExaminationResult examinationResult)
        {
            using (var db = new CardioContext())
            {
                db.ExaminationResults.Remove(examinationResult);
                db.SaveChanges();
            }
            return examinationResult;
        }
        #endregion

        #region MedicalRecords

        public List<MedicalRecord> GetAllUserMedicalRecords(int userId)
        {
            List<MedicalRecord> medicalRecords;
            using (var db = new CardioContext())
            {
                medicalRecords = db.MedicalRecords.Where(p => p.UserId == userId)
                    .OrderByDescending(p => p.MedicalRecordId).ToList();
            }
            return medicalRecords;
        }

        public MedicalRecord AddUserMedicalRecord(int userId)
        {
            MedicalRecord medicalRecord;
            using (var db = new CardioContext())
            {
                medicalRecord = new MedicalRecord
                {
                    MedicalRecordDate = DateTime.Now,
                    UserId = userId
                };
                db.MedicalRecords.Add(medicalRecord);
                db.SaveChanges();
            }
            return medicalRecord;
        }

        public MedicalRecord AddMedicalRecord(MedicalRecord medicalRecord)
        {
            using (var db = new CardioContext())
            {
                db.MedicalRecords.Add(medicalRecord);
                db.SaveChanges();
            }
            return medicalRecord;
        }

        public MedicalRecord AddMedicalRecord(int userId, DateTime medicalRecordDate)
        {
            var medicalRecord = new MedicalRecord()
            {
                UserId = userId,
                MedicalRecordDate = medicalRecordDate
            };
            AddMedicalRecord(medicalRecord);
            return medicalRecord;
        }

        public MedicalRecord GetLastMedicalRecord()
        {
            MedicalRecord medicalRecord;
            using (var db = new CardioContext())
            {
                medicalRecord = db.MedicalRecords.OrderByDescending(p => p.MedicalRecordId).FirstOrDefault();
            }
            return medicalRecord;
        }

        public MedicalRecord GetCurrentUserMedicalRecord(int userId)
        {
            MedicalRecord currentUserMedicalRecord;
            using (var db = new CardioContext())
            {
                currentUserMedicalRecord = db.MedicalRecords.Where(p => p.UserId == userId)
                    .OrderByDescending(p => p.MedicalRecordId).FirstOrDefault();
            }
            return currentUserMedicalRecord;
        }

        public List<MedicalRecord> GetAllMedicalRecords()
        {
            List<MedicalRecord> medicalRecords;
            using (var db = new CardioContext())
            {
                medicalRecords = db.MedicalRecords.ToList();
            }
            return medicalRecords;
        }

        public List<MedicalRecord> GetAllMedicalRecord2Users()
        {
            List<MedicalRecord> medicalRecords;
            using (var db = new CardioContext())
            {
                medicalRecords = db.MedicalRecords.Include(m => m.User).ToList();
            }
            return medicalRecords;
        }

        public MedicalRecord GetMedicalRecordById(int id)
        {
            MedicalRecord medicalRecord;
            using (var db = new CardioContext())
            {
                medicalRecord = db.MedicalRecords.Find(id);
            }
            return medicalRecord;
        }

        public MedicalRecord EditMedicalRecord(MedicalRecord medicalRecord)
        {
            using (var db = new CardioContext())
            {
                db.Entry(medicalRecord).State = EntityState.Modified;
                db.SaveChanges();
            }
            return medicalRecord;
        }

        public MedicalRecord EditMedicalRecord(int medicalRecordId, int userId, DateTime medicalRecordDate)
        {
            var medicalRecord = GetMedicalRecordById(medicalRecordId);
            medicalRecord.UserId = userId;
            medicalRecord.MedicalRecordDate = medicalRecordDate;
            EditMedicalRecord(medicalRecord);
            return medicalRecord;
        }

        public MedicalRecord DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            using (var db = new CardioContext())
            {
                db.MedicalRecords.Remove(medicalRecord);
                db.SaveChanges();
            }
            return medicalRecord;
        }

        public void DeleteMedicalRecord(int medicalRecordId)
        {
            var medicalRecord = GetMedicalRecordById(medicalRecordId);

            using (var db = new CardioContext())
            {
                db.MedicalRecords.Attach(medicalRecord);
                db.MedicalRecords.Remove(medicalRecord);
                db.SaveChanges();
            }
            return;
        }
        #endregion       
    }
}