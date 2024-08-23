import { Route, Routes } from "react-router-dom";
import "./App.css";
import Languages from "./pages/Languages";
import LanguageWords from "./pages/LanguageWords";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Languages />} />
        <Route path="/language-words/:languageId" element={<LanguageWords />} />
      </Routes>
    </>
  );
}

export default App;
