			dotnet test ..\OnlineShop.Products.sln --collect:"Xplat Code Coverage"
			
			tools\reportgenerator.exe ^ "-reports:**\coverage.cobertura.xml" ^ "-targetdir:CoverageReport" ^ -reporttypes:HTML;HTMLSummary
			start CoverageReport\index.htm