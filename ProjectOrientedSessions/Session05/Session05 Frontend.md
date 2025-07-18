
# 🛠️ Task Checklist
## Preparation 
- [ ] Watch https://www.youtube.com/watch?v=SqcY0GlETPk&ab_channel=ProgrammingwithMosh
## 🚧Branching (Project Setup)
- [ ] Create the develop branch 
- [ ] Create the feature/project-setup branch based on develop

## Creating a project (using Vite)

```
npm create vite@latest alibabaclone-frontend --template react-ts
```

## React Folder Structure
- [ ] Create the folders as shown in the picture below
![[Pasted image 20250427154917.png]]
- [ ] move `App.tsx` and `App.css` to 'shared/layout/'
- [ ] adjust the dependencies in these files and `index.html`

## Installing packages
- [ ] run this command to install these packages: `uuid`, `react-router-dom`, and `axios`
```bash
npm install uuid react-router-dom axios
```
- [ ] run this command to install redux
```bash
npm install @reduxjs/toolkit react-redux
```

## CSS/Component Library
There are different ways to handle `CSS` styling when creating components.  
You can choose any approach you prefer:
1. **Plain CSS**
    - Write regular `.css` files and import them into your components.
2. **CSS Modules**
    - Create component-specific `.module.css` files to automatically scope styles.
3. `**TailwindCSS**`
    - A utility-first CSS framework where you apply classes directly in your HTML/JSX.   
4. **Sass / SCSS**
    - An extension of CSS with variables, nesting, and more features. Can be used alone or with modules.
5. **PostCSS**
    - A tool for transforming CSS with JavaScript plugins (often used behind the scenes).
6. **Framework-Specific UI Libraries** (which come with their own styles)
    - Example: Material-UI (MUI), Ant Design, Chakra UI, etc.
    - These often include ready-made components with built-in styling.

- [ ] install the css/component library of your choice 
### Tailwind
If you use tailwind with ShadCN, use this link (skip the first step)
[Tailwind with ShadCN](https://ui.shadcn.com/docs/installation/vite)


## 🚧Merge 
- [ ] Create a PR and merge the current branch with develop
## 🚧Branching 
- [ ] Create the feature/transportation-search branch based on develop
## Creating Models 
- [ ] Create models for the DTOs that are used to convey data between backend and frontend
📂 `Suggested Folder: shared/models/[RelatedFolder]`

## Example:
```ts
export interface TransportationSearchRequest{
    vehicleTypeId ?: number;
    fromCityId ?: number;
    toCityId ?: number;
    startDate ?: Date | null;
    endDate ?: Date | null;
}
```

## Handling API Calls

- [ ]  Create `agent.ts` to handle API calls using axios
📂 Suggested Folder: shared/api/
> Note: the name and the location of this file has changed since writing this note
- [ ] Adjust the `baseURL` to address your web API port
```ts
import axios, { AxiosResponse } from 'axios';

import { TransportationSearchRequest } from '../models/transportation/transportationSearchRequest';
import { TransportationSearchResult } from '../models/transportation/transportationSearchResult';

import { City } from '../models/location/city';

axios.defaults.baseURL = 'https://localhost:[REPLACE THIS WITH YOUR BACKEND WEB API PORT]/api';

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const request = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const TransportationSearch = {
    search: (data: TransportationSearchRequest) => request.post<TransportationSearchResult[]>('/transportation/search', data),
}

const Cities = {
    list: () => request.get<City[]>('/city'),
}
  

const agent = {
    TransportationSearch,
    Cities
}

export default agent;
```

## Creating `CityDropdown` Component
📂 Suggested Folder: features/city/
- [ ] Create components for the parts of the UI you need

```ts
import agent from "@/shared/api/agent";
import { City } from "@/shared/models/location/city";
import { useEffect, useState } from "react";

const CityDropdown = () => {
  const [cities, setCities] = useState<City[]>([]);
  const [selectedCity, setSelectedCity] = useState<number | undefined>();

  useEffect(() => {
    agent.Cities.list()
      .then(setCities)
      .catch((err) => console.error("error loading cities", err));
  }, []);

  return (
    <select
      value={selectedCity}
      onChange={(e) => setSelectedCity(Number(e.target.value))}
    >

      <option value="">Select a City</option>
      {cities.map((city) => (
        <option key={city.id} value={city.id}>
          {city.title}
        </option>
      ))}
    </select>
  );
};

export default CityDropdown;
```
- [ ] **See the analysis of `CityDropdown` component code in the additional info**
## Creating `transportationCard` Component
```ts
import { TransportationSearchResult } from "@/shared/models/transportation/transportationSearchResult";
import React from "react";

interface Props {
  transportation: TransportationSearchResult;
}

const TransportationCard: React.FC<Props> = ({ transportation }) => {
  return (
    <div className="flex items-center justify-between p-4 border rounded-md shadow-md mb-4">
      {/* Price and Select Button */}
      <div className="flex flex-col items-center">
        <div className="text-blue-600 font-bold text-lg">
          {transportation.price} Toman
        </div>
        <button className="bg-blue-500 text-white px-4 py-2 rounded-md mt-2 hover:bg-blue-600">
          Select Ticket
        </button>
      </div>
      {/* Trip Info */}
      <div className="flex-1 mx-4 text-center">
        <div className="font-semibold text-gray-700">
          {transportation.companyTitle}
        </div>
        <div className="flex items-center justify-center mt-2">
          <div className="mx-2">{transportation.fromCityTitle}</div>
          <span className="text-gray-400">→</span>
          <div className="mx-2">{transportation.toCityTitle}</div>
        </div>

        <div className="text-sm text-gray-500 mt-1">
          {new Date(transportation.startDateTime).toLocaleDateString("en", {
            hour: "2-digit",
            minute: "2-digit",
          })}
        </div>
      </div>

      {/* Company Logo or Placeholder */}
      <div className="w-12 h-12">
        <img
          src="/images/company-placeholder.png"
          alt="company"
          className="w-full h-full object-contain"
        />

      </div>
    </div>
  );
};

export default TransportationCard;
```

## Creating `transportationSearchForm` Component

See the code from:
https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation


# 🧠 Hints & Notes
# 🙌 Acknowledgements

- ChatGPT for snippet refinement and explanations
# 🔍 References
- [ ] Check out [[Session05 Additional Info]]
- [ ] Watch for [React](https://www.youtube.com/watch?v=SqcY0GlETPk&ab_channel=ProgrammingwithMosh)
