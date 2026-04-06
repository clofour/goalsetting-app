import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { MantineProvider, createTheme } from '@mantine/core';
import '@mantine/core/styles.css';
import { BrowserRouter, Routes, Route, Navigate } from "react-router";
import './index.css'
import AuthRequired from './AuthRequired.jsx'
import NotFound from './pages/NotFound.jsx'
import Authentication from './pages/Authentication.jsx'
import App from './App.jsx'
import Dashboard from './pages/Dashboard.jsx'
import Calendar from './pages/Calendar.jsx'
import Goals from './pages/Goals.jsx'

const theme = createTheme({
  fontFamily: 'Inter'
})

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <MantineProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route index element={<AuthRequired />} />

          <Route path="app" element={<App />}>
            <Route index element={<Navigate to="dashboard" replace />} />
            <Route path="dashboard" element={<Dashboard />} />
            <Route path="calendar" element={<Calendar />} />
            <Route path="goals" element={<Goals />} />
          </Route>

          <Route path="auth">
            <Route index element={<Navigate to="signin" replace />} />
            <Route path=":tabValue" element={<Authentication />} />
          </Route>

          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </MantineProvider>
  </StrictMode>,
)
