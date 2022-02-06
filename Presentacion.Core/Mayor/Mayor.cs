using IServicios.Caja;
using IServicios.Caja.DTOs;
using PresentacionBase.Formularios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion.Core.Mayor
{
    public partial class Mayor : FormBase
    {
        private readonly ICajaServicio _cajaServicio;
        List<CajaDto> caja;
        bool esDistintoCero = false;
        int contador=0;
        decimal ingresoEnero, ingresoFebrero, ingresoMarzo, ingresoAbril, ingresoMayo,
            ingresoJunio, ingresoJulio, ingresoAgosto, ingresoSeptiembre, ingresoOctubre,
            ingresoNoviembre, ingresoDiciembre,
            egresoEnero, egresoFebrero, egresoMarzo, egresoAbril, egresoMayo,
            egresoJunio, egresoJulio, egresoAgosto, egresoSeptiembre, egresoOctubre,
            egresoNoviembre, egresoDiciembre;
        decimal valance;
        
        decimal totalEfectivoIngresos = 0, totalEfectivoEgresos = 0;
        List<MesContableDto> objetos;
        public Mayor(ICajaServicio cajaServicio)
        {
            InitializeComponent();
            _cajaServicio = cajaServicio;
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            MesContableDto MesContableEnero = new MesContableDto();
            MesContableDto MesContableFebrero = new MesContableDto();
            MesContableDto MesContableMarzo = new MesContableDto();
            MesContableDto MesContableAbril= new MesContableDto();
            MesContableDto MesContableMayo= new MesContableDto();
            MesContableDto MesContableJunio= new MesContableDto();
            MesContableDto MesContableJulio= new MesContableDto();
            MesContableDto MesContableAgosto= new MesContableDto();
            MesContableDto MesContableSeptiembre= new MesContableDto();
            MesContableDto MesContableOctubre= new MesContableDto();
            MesContableDto MesContableNoviembre = new MesContableDto();
            MesContableDto MesContableDiciembre= new MesContableDto();

            if (chkFecha.Checked == true)
            {
                caja = _cajaServicio.ObtenerCajaMayor(false, dtpFechaDesde.Value, dtpFechaHasta.Value);
            }
            else
            {
                caja =_cajaServicio.ObtenerCajaMayor(false, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1));
                int countCaja = 0;
                //Series series;
                //series = chart1.Series.Add("Total Efectivo Ingresos");
                //series.ChartType = SeriesChartType.Spline;
                //series = chart1.Series.Add("Total Salida Efectivo");
                //series.ChartType = SeriesChartType.Spline;
                
                foreach (var item in caja)
                {
                    countCaja++;
                }

                foreach (var item in caja)
                {
                    //if (caja[contador - 1].FechaApertura.Month == caja[contador].FechaApertura.Month)
                    //{
                    if (item.FechaApertura.Month == 1)
                    {
                        MesContableEnero.Año = item.FechaApertura.Year;
                        MesContableEnero.Mes = item.FechaApertura.Month;
                        MesContableEnero.MesStr = "Enero";
                        MesContableEnero.totalIngreso = ingresoEnero += item.TotalEntradaEfectivo;
                        MesContableEnero.totalEgreso = egresoEnero += item.TotalSalidaEfectivo;

                    }
                    if (item.FechaApertura.Month == 2)
                    {
                        MesContableFebrero.Año = item.FechaApertura.Year;
                        MesContableFebrero.Mes = item.FechaApertura.Month;
                        MesContableFebrero.MesStr = "Febrero";
                        MesContableFebrero.totalIngreso = ingresoFebrero += item.TotalEntradaEfectivo;
                        MesContableFebrero.totalEgreso = egresoFebrero += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 3)
                    {
                        MesContableMarzo.Año = item.FechaApertura.Year;
                        MesContableMarzo.Mes = item.FechaApertura.Month;
                        MesContableMarzo.MesStr = "Marzo";
                        MesContableMarzo.totalIngreso = ingresoMarzo += item.TotalEntradaEfectivo;
                        MesContableMarzo.totalEgreso = egresoMarzo += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 4)
                    {
                        MesContableAbril.Año = item.FechaApertura.Year;
                        MesContableAbril.Mes = item.FechaApertura.Month;
                        MesContableAbril.MesStr = "Abril";
                        MesContableAbril.totalIngreso = ingresoAbril += item.TotalEntradaEfectivo;
                        MesContableAbril.totalEgreso = egresoAbril += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 5)
                    {
                        MesContableMayo.Año = item.FechaApertura.Year;
                        MesContableMayo.Mes = item.FechaApertura.Month;
                        MesContableMayo.MesStr = "Mayo";
                        MesContableMayo.totalIngreso = ingresoMayo += item.TotalEntradaEfectivo;
                        MesContableMayo.totalEgreso = egresoMayo += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 6)
                    {
                        MesContableJunio.Año = item.FechaApertura.Year;
                        MesContableJunio.Mes = item.FechaApertura.Month;
                        MesContableJunio.MesStr = "Junio";
                        MesContableJunio.totalIngreso = ingresoJunio += item.TotalEntradaEfectivo;
                        MesContableJunio.totalEgreso = egresoJunio += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 7)
                    {
                        MesContableJulio.Año = item.FechaApertura.Year;
                        MesContableJulio.Mes = item.FechaApertura.Month;
                        MesContableJulio.MesStr = "Julio";
                        MesContableJulio.totalIngreso = ingresoJulio += item.TotalEntradaEfectivo;
                        MesContableJulio.totalEgreso = egresoJulio += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 8)
                    {
                        MesContableAgosto.Año = item.FechaApertura.Year;
                        MesContableAgosto.Mes = item.FechaApertura.Month;
                        MesContableAgosto.MesStr = "Agosto";
                        MesContableAgosto.totalIngreso = ingresoAgosto += item.TotalEntradaEfectivo;
                        MesContableAgosto.totalEgreso = egresoAgosto += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 9)
                    {
                        MesContableSeptiembre.Año = item.FechaApertura.Year;
                        MesContableSeptiembre.Mes = item.FechaApertura.Month;
                        MesContableSeptiembre.MesStr = "Septiembre";
                        MesContableSeptiembre.totalIngreso = ingresoSeptiembre += item.TotalEntradaEfectivo;
                        MesContableSeptiembre.totalEgreso = egresoSeptiembre += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 10)
                    {
                        MesContableOctubre.Año = item.FechaApertura.Year;
                        MesContableOctubre.Mes = item.FechaApertura.Month;
                        MesContableOctubre.MesStr = "Octubre";
                        MesContableOctubre.totalIngreso = ingresoOctubre += item.TotalEntradaEfectivo;
                        MesContableOctubre.totalEgreso = egresoOctubre += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 11)
                    {
                        MesContableNoviembre.Año = item.FechaApertura.Year;
                        MesContableNoviembre.Mes = item.FechaApertura.Month;
                        MesContableNoviembre.MesStr = "Noviembre";
                        MesContableNoviembre.totalIngreso = ingresoNoviembre += item.TotalEntradaEfectivo;
                        MesContableNoviembre.totalEgreso = egresoNoviembre += item.TotalSalidaEfectivo;
                    }
                    if (item.FechaApertura.Month == 12)
                    {
                        MesContableDiciembre.Año = item.FechaApertura.Year;
                        MesContableDiciembre.Mes = item.FechaApertura.Month;
                        MesContableDiciembre.MesStr = "Diciembre";
                        MesContableDiciembre.totalIngreso = ingresoDiciembre += item.TotalEntradaEfectivo;
                        MesContableDiciembre.totalEgreso = egresoDiciembre += item.TotalSalidaEfectivo;
                    }
                    
                }

                #region ListaMesesContables
                List<MesContableDto> ingresos = new List<MesContableDto>();
                ingresos.Add(MesContableEnero);
                ingresos.Add(MesContableFebrero);
                ingresos.Add(MesContableMarzo);
                ingresos.Add(MesContableAbril);
                ingresos.Add(MesContableMayo);
                ingresos.Add(MesContableJunio);
                ingresos.Add(MesContableJulio);
                ingresos.Add(MesContableAgosto);
                ingresos.Add(MesContableSeptiembre);
                ingresos.Add(MesContableOctubre);
                ingresos.Add(MesContableNoviembre);
                ingresos.Add(MesContableDiciembre);
                #endregion
                
                foreach (var item in ingresos)
                {
                    this.chart1.Series["Ingresos"].Points.AddXY(item.MesStr.ToString(), item.totalIngreso);
                    this.chart1.Series["Egresos"].Points.AddXY(item.MesStr.ToString(), item.totalEgreso);

                    //series.Points.Add(double.Parse(item.totalIngreso.ToString()));

                    //series.Label = item.totalIngreso.ToString();
                }
                //totalEfectivoIngresos += item.TotalEntradaEfectivo;
                //totalEfectivoEgresos += item.TotalSalidaEfectivo;
                //series.Label = totalEfectivoIngresos.ToString();
                //series.Label = totalEfectivoEgresos.ToString();

                //series.Points.Add(double.Parse(totalEfectivoIngresos.ToString()));
                //series.Points.Add(double.Parse(totalEfectivoEgresos.ToString()));

                //valance = ingresoEnero + ingresoFebrero + ingresoMarzo + ingresoAbril + ingresoMayo +
                //ingresoJunio + ingresoJulio + ingresoAgosto + ingresoSeptiembre + ingresoOctubre +
                //ingresoNoviembre + ingresoDiciembre +
                //egresoEnero + egresoFebrero + egresoMarzo + egresoAbril + egresoMayo +
                //egresoJunio + egresoJulio + egresoAgosto + egresoSeptiembre + egresoOctubre +
                //egresoNoviembre + egresoDiciembre;

                //this.chart1.Series["Series1"].Points.AddXY("1", totalEfectivoIngresos);
                //this.chart1.Series["Series1"].Points.AddXY("2", totalEfectivoEgresos);



                //series = chart1.Series.Add("Total Efectivo Ingresos");
                //series = chart1.Series.Add("Total Salida Efectivo");

                //series.Label = totalEfectivoIngresos.ToString();
                //series.Label = totalEfectivoEgresos.ToString();

                //series.Points.Add(double.Parse(totalEfectivoIngresos.ToString()));
                //series.Points.Add(double.Parse(totalEfectivoEgresos.ToString()));
            }
        }
    }
}
