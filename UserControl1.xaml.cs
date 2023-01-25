using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Image = VMS.TPS.Common.Model.API.Image;



namespace AUTOSBRT
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public ScriptContext context;

        public void ComboBox(Patient p)
        {

        }





        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {





        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {







        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var seleccion = Cb1.SelectedIndex;


            if (seleccion == 0)
            {
                 casa.Content = "SBRT PROSTATA AUTOMATICO"; ///esta etiqueta esta en usercontrol1.xaml en una posicion determinada    content==le cambia el contenido 



                Patient Paciente = context.Patient;               // llama al paciente 
                const string PTV_ID = "PTV_36.25";                // define una palabra la cual es el nombre del ptv asi tal cual de estar escrito en el plan de tratamiento
                //const string PTV_ID = "PTV_36.25";
                //X => X.Id == "CT_1" && X.Image.Id == "CT_1"

                StructureSet ss = Paciente.StructureSets.FirstOrDefault();    /// aqui seleciona el primer CT

                Structure ptv = ss.Structures.FirstOrDefault(x => x.Id == PTV_ID);  /// aqui selciona la estructura PTV 


                Course SelectedCourse = Paciente.Courses.FirstOrDefault(x => x.Id == "Curso1");

                Paciente.BeginModifications();






                //  StructureSet ss = Paciente.CopyImageFromOtherPatient("abcd1234", "Phantom1", "PHANTOM");
                //Seleccionamos la estructura
                //  var ptv = Paciente.StructureSets.First().Structures.FirstOrDefault();
                ExternalPlanSetup pln = Paciente.Courses.FirstOrDefault(x => x.Id == SelectedCourse.Id).AddExternalPlanSetup(Paciente.StructureSets.FirstOrDefault());
                //   pln.Id = NombrePlan;
                pln.SetPrescription(int.Parse("100"), new DoseValue(double.Parse("100"), "cGy"), 1);
                //Establecemos el isocentro
                var s = Paciente.StructureSets.FirstOrDefault().Structures.Where(x => x.Id.ToUpper() == "PTV_36.25").FirstOrDefault();
                //    var isocentro = new VVector(ptv.CenterPoi);
                //Parametros de la maquina

                var ebmp = new ExternalBeamMachineParameters("TrueBeamSN3556", "6X", 300, "STATIC", null);//(maquina, energia, tasa de dosis, tecnica, modelo fluencia primaria)
                                                                                                          //creacion de campos
                Beam campo1 = pln.AddStaticBeam(ebmp, new VRect<double>(-200, -125, 200, 125), 0, 270, 0, ptv.CenterPoint);
                var editparcamp1 = campo1.GetEditableParameters();

                ///////////////////////////////////////////////////////////////////////
                ///

                //  ExternalBeamMachineParameters machineParameters =
                // new ExternalBeamMachineParameters(unitName, "6X", 600, "STATIC", string.Empty);

                VVector isocenter = pln.Beams.FirstOrDefault().IsocenterPosition;

                //Add setup fields
                //  Beam setup_ant = pln.AddSetupBeam(ebmp, new VRect<double>(-120, -120, 120, 120), 0, 0, 0, isocenter);
                //  setup_ant.Id = string.Format("VER-ANT");
                //  Beam setup_lat = pln.AddSetupBeam(ebmp, new VRect<double>(-120, -120, 120, 120), 0, 270, 0, isocenter);
                //  setup_lat.Id = string.Format("VER-LAT");
                Beam cbct = pln.AddSetupBeam(ebmp, new VRect<double>(-100, -100, 100, 100), 0, 0, 0, isocenter);
                cbct.Id = string.Format("CBCT");

                var myDRR = new DRRCalculationParameters(500); // 500mm is the DRR size

                myDRR.SetLayerParameters(0, 1, 100, 1000);


                // setup_ant.CreateOrReplaceDRR(myDRR);
                // setup_lat.CreateOrReplaceDRR(myDRR);




















                //  Beam campo2 = pln.AddStaticBeam(ebmp, new VRect<double>(-200, -125, 200, 125), 0, 270, 0, isocentro);
                //  var editparcamp2 = campo2.GetEditableParameters();
                //  MessageBox.Show("Calculado dosis");
                //  pln.CalculateDose();

                //  editparcamp1.WeightFactor = double.Parse(UM) / campo1.Meterset.Value;
                //   editparcamp2.WeightFactor = double.Parse(UM) / campo2.Meterset.Value;

                //   campo1.ApplyParameters(editparcamp1);
                //  campo2.ApplyParameters(editparcamp2);
                //   _app.SaveModifications();
                MessageBox.Show("El PLAN y las VERIFICADORAS han sido creadas");


                /////////////////////////////////////////////// hasta aqui crea el plan ///////////////////////////////////////////




                // Patient patient = context.Patient;
                //
                // if (patient == null)
                // {
                //     throw new ApplicationException("Por favor, cargue un paciente");
                // }
                //
                // ExternalPlanSetup plan = context.ExternalPlanSetup;
                //
                // if (plan == null)
                // {
                //     throw new ApplicationException("Cargue un plan");
                // }
                //
                // patient.BeginModifications();
                //
                // VVector isocenter = plan.Beams.FirstOrDefault().IsocenterPosition;
                // ExternalBeamTreatmentUnit unit = plan.Beams.FirstOrDefault().TreatmentUnit;
                //
                // String unitName = unit.ToString();
                // if (unitName.Contains(""))
                //     unitName = "TrueBeamSN3556";
                // else if (unitName.Contains(""))
                //     unitName = "TruebeamSN5377";
                // // else
                // //{
                // // MessageBox.Show("MAQUINA DESCONOCIDA");
                // //  return;
                //
                // // }
                //
                // ExternalBeamMachineParameters machineParameters =
                //    new ExternalBeamMachineParameters(unitName, "6X", 600, "STATIC", string.Empty);
                //
                //
                //
                // //Add setup fields
                // Beam setup_ant = plan.AddSetupBeam(machineParameters, new VRect<double>(-120, -120, 120, 120), 0, 0, 0, isocenter);
                // setup_ant.Id = string.Format("VER-ANT");
                // Beam setup_lat = plan.AddSetupBeam(machineParameters, new VRect<double>(-120, -120, 120, 120), 0, 270, 0, isocenter);
                // setup_lat.Id = string.Format("VER-LAT");
                // Beam cbct = plan.AddSetupBeam(machineParameters, new VRect<double>(-100, -100, 100, 100), 0, 0, 0, isocenter);
                // cbct.Id = string.Format("CBCT");
                // //
                //   var myDRR = new DRRCalculationParameters(500); // 500mm is the DRR size
                //
                //   myDRR.SetLayerParameters(0, 1, 100, 1000);
                //
                //
                //   setup_ant.CreateOrReplaceDRR(myDRR);
                //   setup_lat.CreateOrReplaceDRR(myDRR);
                //
                //
                //



























                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                  


                // StructureSet estructura = context.Patient.StructureSets.Where(ss => ss.Id == "prostata").FirstOrDefault(); /// llamo al CT prostata //
                //                                                                                                                                   //
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
              



                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                                                                                                                                  //
                //ahora dentro de este CT vamos a tener varias estructuras y para crear el plan necesitamos el vector del isocentro                 //
                //asi que llamamos al Id del ptv de la manera que aparece aqui abajo                                                                //
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                //const string PTV_ID = "PTV_36.25"; //////////// llamo al Id del ptv "ojo no confundir con el name ya que eso va depender del medico"
                // Structure ptv = estructura.Structures.FirstOrDefault(x => x.Id == ptvid);

                // Patient patient = context.Patient;

                // patient.BeginModifications();  ////////// permite realizar moficaciones al paciente

                // Structure ptv = ss.Structures.FirstOrDefault(x => x.Id == PTV_ID);


                // Course curso = context.Patient.Courses.FirstOrDefault();  //////// vaya al paciente y selecione el primer curso por defecto





                //  ExternalPlanSetup SBRTPR_36Gy = curso.AddExternalPlanSetup(estructura); ////////////tome el Ct prostata y lo agregue al plan 
                //
                //  ExternalBeamMachineParameters parametrosdelhaz = new ExternalBeamMachineParameters("TrueBeamSN3556" ,"6FFF" , 600, "SRS Arc Therapy",null ); ///creamos los paramterosde la maquina 
                //
                //  Beam beam = SBRTPR_36Gy.AddArcBeam(parametrosdelhaz, new VRect<double>(-50, -50, -50, -50),0,181,179,GantryDirection.Clockwise,0, ptv.CenterPoint) ; ////crea los parametros del haz 
                //
                //  beam.FitCollimatorToStructure(new FitToStructureMargins (5), ptv,true,true,false);



            }
            if (seleccion == 1)
            {
                casa.Content = "CREAR ESTRUCTURA HDA"; 

                Patient patient = context.Patient;  // call the variable patient
                patient.BeginModifications(); // enable the write true in  patient
                StructureSet ss = context.StructureSet;  /// selection CT
                //I will use the copy to find my HU-Pixels with CreateAndSearchBody without running in approval issues
                //StructureSet ss_Copy = ss.();

                if (ss == null)     // if patient not have CT show a message "THE STRUTURES IS NOT FOUND " and close the script
                {
                    MessageBox.Show("LAS ESTRUCTIRAS NO FUERON ENCONTRADAS.");
                    return;
                }
                Image image = ss.Image;   


                Structure newStr = ss.AddStructure("Control", "HDA");   //create the struture of high density but is not counturning 

                Structure body = ss.Structures.Where(x => !x.IsEmpty && (x.DicomType.ToUpper().Equals("EXTERNAL") || x.DicomType.ToUpper().Equals("BODY") || x.Id.ToUpper().Equals("KÖRPER") || x.Id.ToUpper().Equals("BODY") || x.Id.ToUpper().Contains("OUTER CONTOUR"))).FirstOrDefault();

                for (int k = 0; k < image.ZSize; k++)
                {
                    body.ClearAllContoursOnImagePlane(k);
                }
                //I did not understand how to create SearchBodyParameters from scratch but changing the default is easy
                var BodyPar = ss.GetDefaultSearchBodyParameters(); 

                BodyPar.PreCloseOpenings = false;
                BodyPar.FillAllCavities = false;
                BodyPar.PreDisconnect = false;
                BodyPar.Smoothing = false;
                //for a directDensity CT this is the appropiate value. For other PlanningCTs it should be higher
                BodyPar.LowerHUThreshold = 1600;

                ss.CreateAndSearchBody(BodyPar);
                newStr.SegmentVolume = body.SegmentVolume;
                //  Image imageDelete = ss.Image;
                //  ss_Copy.Delete();
                //images cannot be deleted with ESAPI. Therefore I will rename it
                // imageDelete.Id = "IgnoreOrDelete";

                string errorMessage = "";
                if (newStr.CanSetAssignedHU(out errorMessage).ToString().ToLower() == "true")
                {
                    newStr.SetAssignedHU(1600);
                    MessageBox.Show(string.Format("Se creó una nueva estructura con los siguientes parámetros: \n\n " +
                        "Id:\t{0}\n" +
                        "Volume:\t{1}\n" +
                        "SeperateParts:\t{2}\n" +
                        "AssignedHU:\t{3}"
                        , newStr.Id, Math.Round(newStr.Volume, 3), newStr.GetNumberOfSeparateParts(), 1600, "Success"));
                }
                else
                {
                    MessageBox.Show(string.Format("New Structure with following parameters was created:\n\n" +
                        "Id:\t{0}\n" +
                        "Volume:\t{1}\n" +
                        "SeperateParts:\t{2}\n" +
                        "AssignedHU:\t{3}\n\n" +
                        "Assign-Error:\t{4}"
                        , newStr.Id, Math.Round(newStr.Volume, 3), newStr.GetNumberOfSeparateParts(), "No", errorMessage), "Success");
                }




            }
            if (seleccion == 2)
            {
                casa.Content = "CREAR VERIFICADORAS Y CBCT";



                Patient patient = context.Patient;

                if (patient == null)
                {
                    throw new ApplicationException("Por favor, cargue un paciente");
                }

                ExternalPlanSetup plan = context.ExternalPlanSetup;

                if (plan == null)
                {
                    throw new ApplicationException("Cargue un plan");
                }

                patient.BeginModifications();

                VVector isocenter = plan.Beams.FirstOrDefault().IsocenterPosition;
                ExternalBeamTreatmentUnit unit = plan.Beams.FirstOrDefault().TreatmentUnit;

                String unitName = unit.ToString();
                if (unitName.Contains(""))
                    unitName = "TrueBeamSN3556";
                else if (unitName.Contains(""))
                    unitName = "TruebeamSN5377";
                // else
                //{
                // MessageBox.Show("MAQUINA DESCONOCIDA");
                //  return;

                // }

                ExternalBeamMachineParameters machineParameters =
                new ExternalBeamMachineParameters(unitName, "6X", 600, "STATIC", string.Empty);



                //Add setup fields
                // Beam setup_ant = plan.AddSetupBeam(machineParameters, new VRect<double>(-120, -120, 120, 120), 0, 0, 0, isocenter);
                // setup_ant.Id = string.Format("VER-ANT");
                //  Beam setup_lat = plan.AddSetupBeam(machineParameters, new VRect<double>(-120, -120, 120, 120), 0, 270, 0, isocenter);
                // setup_lat.Id = string.Format("VER-LAT");
                Beam cbct = plan.AddSetupBeam(machineParameters, new VRect<double>(-100, -100, 100, 100), 0, 0, 0, isocenter);
                cbct.Id = string.Format("CBCT");

                var myDRR = new DRRCalculationParameters(500); // 500mm is the DRR size

                myDRR.SetLayerParameters(0, 1, 100, 1000);


                // setup_ant.CreateOrReplaceDRR(myDRR);
                // setup_lat.CreateOrReplaceDRR(myDRR);





                MessageBox.Show("Se han creado la verificadora CBCT ");





            }
            if (seleccion == 3)
            {
                casa.Content = "CREAR PLAN";   

                const string PTV_ID = "PTV_36.25";
                Patient Paciente = context.Patient;

                //const string PTV_ID = "PTV_36.25";
                //X => X.Id == "CT_1" && X.Image.Id == "CT_1"

                StructureSet ss = Paciente.StructureSets.FirstOrDefault();

                Structure ptv = ss.Structures.FirstOrDefault(x => x.Id == PTV_ID);


                Course SelectedCourse = Paciente.Courses.FirstOrDefault(x => x.Id == "Curso1");

                Paciente.BeginModifications();






                //  StructureSet ss = Paciente.CopyImageFromOtherPatient("abcd1234", "Phantom1", "PHANTOM");
                //Seleccionamos la estructura
                //  var ptv = Paciente.StructureSets.First().Structures.FirstOrDefault();
                ExternalPlanSetup pln = Paciente.Courses.FirstOrDefault(x => x.Id == SelectedCourse.Id).AddExternalPlanSetup(Paciente.StructureSets.FirstOrDefault());
                //   pln.Id = NombrePlan;
                pln.SetPrescription(int.Parse("100"), new DoseValue(double.Parse("100"), "cGy"), 1);
                //Establecemos el isocentro
                var s = Paciente.StructureSets.FirstOrDefault().Structures.Where(x => x.Id.ToUpper() == "PTV_36.25").FirstOrDefault();
                //    var isocentro = new VVector(ptv.CenterPoi);
                //Parametros de la maquina

                var ebmp = new ExternalBeamMachineParameters("TrueBeamSN3556", "6X", 300, "STATIC", null);//(maquina, energia, tasa de dosis, tecnica, modelo fluencia primaria)
                                                                                                          //creacion de campos

                Beam campo1 = pln.AddStaticBeam(ebmp, new VRect<double>(-200, -125, 200, 125), 0, 270, 0, ptv.CenterPoint);
                var editparcamp1 = campo1.GetEditableParameters();
                //

                pln.Id = string.Format("SBRTPR36.25Gy");
              
                
                
                //  Beam campo2 = pln.AddStaticBeam(ebmp, new VRect<double>(-200, -125, 200, 125), 0, 270, 0, isocentro);
                //  var editparcamp2 = campo2.GetEditableParameters();
                //  MessageBox.Show("Calculado dosis");
                //  pln.CalculateDose();

                //  editparcamp1.WeightFactor = double.Parse(UM) / campo1.Meterset.Value;
                //   editparcamp2.WeightFactor = double.Parse(UM) / campo2.Meterset.Value;

                //   campo1.ApplyParameters(editparcamp1);
                //  campo2.ApplyParameters(editparcamp2);
                //   _app.SaveModifications();
                MessageBox.Show("El Plan ha sido creado");































            }
            if (seleccion == 4)
            {
                casa.Content = "CREAR ESTRUCTURAS Y VERIFICACION DE ESTRUCTURAS ";



                const string PTV_ID = "PTV_36.25";
                const string RECTUM_ID = "Recto";
                const string URETHRA_ID = "Uretra";
                const string GTV_ID = "GTV";
                const string EXPANDED_PTV_ID = "ring1";
                const string EXPANDED_PTV2_ID = "ring2";
                const string RECTUM_OPT_ID = "anillo";
                const string Purethra_OPT_ID = "uretra intraprostatica";
                const string SCRIPT_NAME = "Opt Structures Script";
                const string OTV_ID = "otv1";
                const string ANALCANAL_ID = "Canal Anal";
                const string vejiga_ID = "Vejiga";
                const string SKIN_ID = "Piel";
                const string CABEZAFEMORALD_ID = "Cab Fem Der";
                const string CABEZAFEMORALI_ID = "Cab Fem Izq";
                const string RECTOANTERIOR_ID = "Recto Anterior";
                const string RECTOPOSTERIOR_ID = "Recto Posterior";
                const string SIGMA_ID = "Sigma";
                //const string bien_ID = "uuuu";

                if (context.Patient == null || context.StructureSet == null)
                {
                    MessageBox.Show("Please load a patient, 3D image, and structure set before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                StructureSet ss = context.StructureSet;




                // find Rectum
                Structure rectum = ss.Structures.FirstOrDefault(x => x.Id == RECTUM_ID);
                if (rectum == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", RECTUM_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }
                Structure colon = ss.Structures.FirstOrDefault(x => x.Id == SIGMA_ID);
                if (colon == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", SIGMA_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }

                Structure rectalwallanterior = ss.Structures.FirstOrDefault(x => x.Id == RECTOANTERIOR_ID);
                if (rectalwallanterior == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", RECTOANTERIOR_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }

                Structure posteriorwallofrectum = ss.Structures.FirstOrDefault(x => x.Id == RECTOPOSTERIOR_ID);
                if (posteriorwallofrectum == null)
                {
                    MessageBox.Show(string.Format("'{0}' REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", RECTOPOSTERIOR_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }

                Structure skin = ss.Structures.FirstOrDefault(x => x.Id == SKIN_ID);
                if (skin == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", SKIN_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }

                Structure gtv = ss.Structures.FirstOrDefault(x => x.Id == GTV_ID);
                if (gtv == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", GTV_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }
                Structure FemoralHeadRight = ss.Structures.FirstOrDefault(x => x.Id == CABEZAFEMORALD_ID);
                if (FemoralHeadRight == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", CABEZAFEMORALD_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);


                }

                Structure Urethra = ss.Structures.FirstOrDefault(x => x.Id == URETHRA_ID);
                if (Urethra == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", URETHRA_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    // return; /// this close the script

                }
                Structure FemoralHeadLeft = ss.Structures.FirstOrDefault(x => x.Id == CABEZAFEMORALI_ID);
                if (FemoralHeadLeft == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", CABEZAFEMORALI_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    //return; /// this close the script
                }

                Structure Analcanal = ss.Structures.FirstOrDefault(x => x.Id == ANALCANAL_ID);
                if (Analcanal == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", ANALCANAL_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    //return;/// this close the script
                }
                Structure Bladder = ss.Structures.FirstOrDefault(x => x.Id == vejiga_ID);
                if (Bladder == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", vejiga_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    // return; /// this close the script
                }

                //Structure ring = ss.Structures.FirstOrDefault(x => x.Id == RING_ID);
                //if (ring == null)
                //{
                // MessageBox.Show(string.Format("'{0}'NO ENCONTRO!", RING_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                // return;
                // }



                //find PTV
                Structure ptv = ss.Structures.FirstOrDefault(x => x.Id == PTV_ID);

                if (ptv == null)
                {
                    MessageBox.Show(string.Format("'{0}'  REVISAR ESTRUCTURA PUEDE ESTAR MAL NOMBRADA O NO EXISTE!", PTV_ID), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    //return; /// this close the script
                }


                if (rectum == null || ptv == null || colon == null || rectalwallanterior == null || posteriorwallofrectum == null || skin == null || FemoralHeadRight == null || FemoralHeadLeft == null ||
                    Analcanal == null || Bladder == null || gtv == null)
                {






                    return;


                }
                else
                {
                    context.Patient.BeginModifications();   // enable writing with this script.



                    // create the empty "ptv+2cm" structure
                    Structure ptv_2cm = ss.AddStructure("PTV", EXPANDED_PTV_ID);
                    Structure ptv_3cm = ss.AddStructure("PTV", EXPANDED_PTV2_ID);
                    // Structure Urehtra = ss.AddStructure("PTV", bien_ID);


                    ptv_2cm.SegmentVolume = ptv.Margin(20.0);
                    ptv_3cm.SegmentVolume = ptv.Margin(30.0);


                    Structure buffered_Ring = ss.AddStructure("AVOIDANCE", RECTUM_OPT_ID);
                    Structure buffered_Urethra = ss.AddStructure("AVOIDANCE", Purethra_OPT_ID);
                    // Structure buffered_Ring = ss.AddStructure("AVOIDANCE",  INTERSECION_PTV3_ID);


                    // calculate overlap structures using Boolean operators.
                    buffered_Ring.SegmentVolume = ptv_3cm.Sub(ptv_2cm);     //'Sub' subtracts overlapping volume of expanded PTV from rectum
                    buffered_Urethra.SegmentVolume = Urethra.And(ptv);      //buffered_Ring.SegmentVolume = ptv_3cm.Sub(ptv_2cm);

                    Structure otv_3mm = ss.AddStructure("PTV", OTV_ID);
                    otv_3mm.SegmentVolume = buffered_Urethra.Margin(3.0);

                    string message = string.Format("{0} volume = {4}\n{1} volume = {5}\n{2} volume = {6}\n{3} volume = {7} ",
                            ptv.Id, otv_3mm.Id, Urethra.Id, ptv_2cm.Id, ptv_3cm.Id, buffered_Ring.Id, buffered_Urethra.Id,
                            ptv.Volume, otv_3mm.Volume, Urethra.Volume, ptv_2cm.Volume, ptv_3cm.Volume, buffered_Ring.Volume, buffered_Urethra.Volume);
                
                    ss.RemoveStructure(ptv_2cm);
                    ss.RemoveStructure(ptv_3cm);



                }

                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    } }
