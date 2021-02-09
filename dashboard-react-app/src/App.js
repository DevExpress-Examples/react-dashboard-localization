import React from 'react';
import './App.css';
import DashboardControl from 'devexpress-dashboard-react';
import { ResourceManager } from 'devexpress-dashboard';
import { locale } from "devextreme/localization";

function App() {  
  var culture = "de";
  ResourceManager.setLocalizationMessages(require(`../json resources/dx-dashboard.${culture}.json`));
  ResourceManager.setLocalizationMessages(require(`../json resources/dx-analytics-core.${culture}.json`));
  
  // Apply culture-specific formatting:
  locale(culture);  
  
  // Localize the specified string at runtime (the "Export To" button's caption in the dashboard title):
  ResourceManager.setLocalizationMessages({ "DashboardStringId.ActionExportTo": "Custom Text for Export Button" });  

  return (
    <div style={{ position : 'absolute', top : '0px', left: '0px', right : '0px', bottom: '0px' }}>
      <DashboardControl style={{ height: '100%' }} 
        endpoint="http://localhost:5000/api/dashboard">
      </DashboardControl>
  </div>
  );
}

export default App;