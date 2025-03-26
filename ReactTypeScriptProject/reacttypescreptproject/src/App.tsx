
import { CssBaseline } from '@mui/material';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from "./Components/Layout/Layout"; 
import { routes as appRoutes } from "./routes";

function App() {
  return (
    <div className="App">
          <CssBaseline />
          <BrowserRouter>
              <Layout>
                  <Routes>
                      {appRoutes.map((route) =>
                      (<Route
                          key={route.key}
                          path={route.path}
                          element={<route.component /> }
                      />
                      ))}
                  </Routes>
              </Layout>
          </BrowserRouter> 
    </div>
  );
}

export default App;
