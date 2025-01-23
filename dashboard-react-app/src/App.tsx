import 'ace-builds/css/ace.css';  
import 'ace-builds/css/theme/dreamweaver.css';  
import 'ace-builds/css/theme/ambiance.css';  
import 'devextreme/dist/css/dx.light.css';
import '@devexpress/analytics-core/dist/css/dx-analytics.common.css';
import '@devexpress/analytics-core/dist/css/dx-analytics.light.css';
import '@devexpress/analytics-core/dist/css/dx-querybuilder.css';
import 'devexpress-dashboard/dist/css/dx-dashboard.light.css';
import DashboardControl from 'devexpress-dashboard-react';
import { ResourceManager } from 'devexpress-dashboard';
import { locale } from "devextreme/localization";

import deAnalyticsMessages from '../jsonResources/dx-analytics-core.de.json';
import deDashboardMessages from '../jsonResources/dx-dashboard.de.json';

function App() {
  // Localize the Web Dashboard UI for the German market (the 'de' culture):
  ResourceManager.setLocalizationMessages(deAnalyticsMessages);
  ResourceManager.setLocalizationMessages(deDashboardMessages);
  
  // Apply culture-specific formatting:
  locale('de');  
  
  // Localize the specified string at runtime (the "Export To" button's caption in the dashboard title):
  ResourceManager.setLocalizationMessages({ "DashboardStringId.ActionExportTo": "Custom Text for Export Button" });

  return (
    <div style={{ position : 'absolute', top : '0px', left: '0px', right : '0px', bottom: '0px' }}>
      <DashboardControl style={{ height: '100%' }} 
        endpoint="https://demos.devexpress.com/services/dashboard/api">
      </DashboardControl>
    </div>
  )
}

export default App
