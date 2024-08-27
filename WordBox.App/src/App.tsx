import { Route, Routes } from "react-router-dom";
import "./App.css";
import Languages from "./pages/Languages";
import LanguageWords from "./pages/LanguageWords";
import WordMeanings from "./pages/WordMeanings";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Languages />} />
        <Route path="/words/:languageId" element={<LanguageWords />} />
        <Route path="/meanings/:wordId" element={<WordMeanings />} />
      </Routes>
    </>
  );
}

export default App;
