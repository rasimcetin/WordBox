export interface LanguageDto {
  id: string;
  name: string;
  code: string;
}

export interface WordDto {
  text: string;
  id: string;
  languageId: string;
}

export interface WordMeaningDto {
  text: string;
  id: string;
  wordId: string;
}

export interface Result<T> {
  isSuccess: boolean;
  data?: T;
  error?: string;
}
