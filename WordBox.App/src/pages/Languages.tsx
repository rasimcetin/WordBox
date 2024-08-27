import { useEffect, useState } from "react";
import { LanguageDto, Result } from "../model/WordBoxModel";
import axios from "axios";
import { Link } from "react-router-dom";

// WordBox.App\src\pages\Languages.tsx

function Languages() {
  const [languages, setLanguages] = useState<LanguageDto[]>([]);
  const [newLanguageName, setNewLanguageName] = useState<string>("");
  const [newLanguageCode, setNewLanguageCode] = useState<string>("");

  // Fetch languages from API
  useEffect(() => {
    async function fetchLanguages() {
      try {
        const response = await axios.get<Result<LanguageDto[]>>(
          "https://localhost:7275/api/languages"
        );

        if (response.data.isSuccess) {
          setLanguages(response.data.data!);
        }
      } catch (error) {
        console.error("Error" + error);
      }
    }
    fetchLanguages();
  }, []);

  async function addLanguage() {
    if (newLanguageName === "" || newLanguageCode === "") {
      return;
    }
    try {
      const response = await axios.post<Result<LanguageDto>>(
        "https://localhost:7275/api/languages",
        { name: newLanguageName, code: newLanguageCode }
      );
      if (response.data.isSuccess) {
        setLanguages([...languages, response.data.data!]);
        setNewLanguageName("");
        setNewLanguageCode("");
      }
    } catch (error) {
      console.error("Error" + error);
    }
  }

  async function deleteLanguage(languageId: string) {
    try {
      const response = await axios.delete<Result<LanguageDto>>(
        `https://localhost:7275/api/languages/${languageId}`
      );
      if (response.data.isSuccess) {
        setLanguages(languages.filter((l) => l.id !== languageId));
      }
    } catch (error) {
      console.error("Error" + error);
    }
  }

  return (
    <div className="container">
      <h1>Languages</h1>
      <div>
        <input
          id="name"
          type="text"
          value={newLanguageName}
          onChange={(e) => setNewLanguageName(e.target.value)}
          placeholder="Enter new language..."
        ></input>
        <input
          id="code"
          type="text"
          value={newLanguageCode}
          onChange={(e) => setNewLanguageCode(e.target.value)}
          placeholder="Enter new language code..."
        />
        <button onClick={addLanguage}>
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
            <path d="M440-440H200v-80h240v-240h80v240h240v80H520v240h-80v-240Z" />
          </svg>
        </button>
      </div>
      <ul className="list">
        {languages.map((l) => (
          <li key={l.id}>
            <Link to={`/words/${l.id}`}>{l.name}</Link>
            <button onClick={() => deleteLanguage(l.id)}>
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
                <path d="M280-120q-33 0-56.5-23.5T200-200v-520h-40v-80h200v-40h240v40h200v80h-40v520q0 33-23.5 56.5T680-120H280Zm400-600H280v520h400v-520ZM360-280h80v-360h-80v360Zm160 0h80v-360h-80v360ZM280-720v520-520Z" />
              </svg>
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Languages;
