namespace Base.Domain.Reports
{
    public static class RelatorioReport
    {

        public enum Orientacao
        {
            Paisagem,
            Retrato
        }
        public static string Html(string nome, string nomeRelatorio, string thead, string tbody, string tfoot, string informacoes = "", string resumo = "", string mensagem = "", Orientacao orientacao = Orientacao.Paisagem)
        {
            string html = @"
            <style>
	            @media print{
		            @page {
			            size: {orientacao};
		            }
		            body {-webkit-print-color-adjust: exact;}
	            }
	            #report {
                    width: {reportWidth};
		            font-family:'Roboto', 'Noto', sans-serif;
	            }
	            table {
		            width: 100%;
		            font-size:12px;
		            border: 1px solid black;
		            border-left:none;
		            border-right:none;
		            border-bottom:none;
	            }
	            table > thead > tr > th {
		            border-bottom: 1px solid black;
		            background: #eaeaea;
	            }
	            table > tbody > tr > td {
		            border-bottom:1px solid darkgray;
	            }
	            table > tbody > tr:nth-child(even) {
		            background-color: #fafafa;
	            }
	            table > tbody > tr:last-child > td {
		            border-bottom: none;
	            }
	            table > tfoot > tr > td {
		            border-top: 1px solid black;
	            }
            </style>
            <center>
                <div id=""report"">
                    <img src=""{brasao}"" style=""float: left; width: 80px; border: none; padding: 5px;padding-bottom:20px;""/>
		            <h1 style=""font-size:14px;text-transform:uppercase;padding-bottom:2px;"">{nome}</h1>
		            <h2 style=""font-size: 11px;font-weight:normal;"">{orgao}</h2>
		            <h2 style=""font-size: 11px;text-transform:uppercase;font-weight:none;"">&nbsp;{informacoes}&nbsp;</h2>
		            <h2 style=""font-size: 10px;font-weight:none;"">&nbsp;{resumo}&nbsp;</h2>
                    <div id=""relatoriogeral"">
		            <table cellspacing=""0"" cellpadding=""5"">
                        <thead style=""text-align:left;"">
                            {thead}
                        </thead>
                        <tbody>
                            {tbody}
                        </tbody>
                        <tfoot>
                            {tfoot}
                        </tfoot>
		            </table>
                   </div>
		            <h2 style=""font-size: 14px;font-weight:bold;"">&nbsp;{mensagem}&nbsp;</h2>
	            </div>
            </center>
            ";


            if (orientacao == Orientacao.Paisagem)
            {
                html = html.Replace("{orientacao}", "landscape");
                html = html.Replace("{reportWidth}", "100%");
            }
            else
            {
                html = html.Replace("{orientacao}", "portrait");
                html = html.Replace("{reportWidth}", "740px");
            }

            //html = html.Replace("{brasao}", linkPrefeitura.Replace("site/", "") + "site/Brasoes/" + idOrgaos + "/cabecalho.png");
            html = html.Replace("{nome}", nomeRelatorio);
            html = html.Replace("{orgao}", nome);
            html = html.Replace("{informacoes}", informacoes);
            html = html.Replace("{resumo}", resumo);
            html = html.Replace("{thead}", thead);
            html = html.Replace("{tbody}", tbody);
            html = html.Replace("{tfoot}", tfoot);
            html = html.Replace("{mensagem}", mensagem);

            return html;
        }
    }
}
