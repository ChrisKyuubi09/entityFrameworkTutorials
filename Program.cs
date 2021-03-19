using efTutorialCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace efTutorialCore
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			//entity framework tutorial
			//Select all rows from Doctors table
			using (var context = new ClinicContext())
			{
				var doctors = context.Doctors.ToList();

				foreach (var obj in doctors)
				{
					Console.WriteLine("Doctors name: " + obj.Dname + "\nCode: " + obj.Dcode + "\nSpecialty: " + obj.Specialtycode + "\n\n");
				}
			}

			//Doctors table JOIN with Specialty
			using (var context2 = new ClinicContext())
			{
				var resultWithSpecialty = context2.Doctors
											.Join(
												context2.Specialties,
													doc => doc.Specialtycode,
													spe => spe.Scode2,
													(doc, spe) => new
													{
														DoctorName = doc.Dname,
														DoctorCode = doc.Dcode,
														Specialty = spe.Description
													}
												);
				foreach (var obj in resultWithSpecialty)
				{
					Console.WriteLine("Doctors name: " + obj.DoctorName + "\nCode: " + obj.DoctorCode + "\nSpecialty: " + obj.Specialty + "\n\n");
				}
			}

			//CRUD Functionality EntityFramework
			//CREATE
			using (var ctx3 = new ClinicContext())
			{
				var newDoctor = new Doctor()
				{
					Dcode = "4",
					Dname = "Davit",
					Specialtycode = "A3"
				};
				//INSERT
				ctx3.Add(newDoctor);
				ctx3.SaveChanges();
			}

			//UPDATE
			using (var ctx4 = new ClinicContext())
			{
				var updateDoctor = ctx4.Doctors.SingleOrDefault(d => d.Dcode == "4");
				if (updateDoctor != null)
				{
					try
					{
						updateDoctor.Dname = "Davit G";
						ctx4.SaveChanges();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.StackTrace.ToString());
					}
				}
				else
				{
					Console.WriteLine("Doctor id not found");
				}
			}

			//DELETE
			using (var ctx5 = new ClinicContext())
			{
				var deleteDoctor = ctx5.Doctors.SingleOrDefault(d => d.Dcode == "4");
				if (deleteDoctor != null)
				{
					try
					{
						ctx5.Doctors.Remove(deleteDoctor);
						ctx5.SaveChanges();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.StackTrace.ToString());
					}
				}
			}

			var Reader = new StreamReader(File.OpenRead("C:\\Users\\Christos\\Desktop\\doctorsUpload.csv"));
			var listDocNames = new List<String>();
			var listDocCodes = new List<String>();

			while (!Reader.EndOfStream) 
			{
				var line = Reader.ReadLine();
				var values = line.Split(',');

				listDocNames.Add(values[0]);
				listDocCodes.Add(values[1]);
			}

			foreach (var x in listDocNames)
			{
				Console.WriteLine(x);
			}

			foreach (var y in listDocCodes)
			{
				Console.WriteLine(y);
			}

			using (var ctx6 = new ClinicContext())
			{
				var allItems = ctx6.Doctors.Select(x => x.Dcode).ToList();
				Console.WriteLine("All items");
				foreach (var x in allItems)
				{
					Console.WriteLine(x);
				}

				var failedItems = ctx6.Doctors.Where(x => !listDocCodes.Contains(x.Dcode)).ToList();

				Console.WriteLine("Failed items");
				foreach (var x in failedItems)
				{
					Console.WriteLine(x.Dcode);
				}
			}

		}
	}
}
