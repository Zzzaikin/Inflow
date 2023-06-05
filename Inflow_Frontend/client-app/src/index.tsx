import ReactDOM from 'react-dom/client';
import App from './components/App';
import {Provider} from "react-redux";
import appStore from "./store/Store";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <Provider store={appStore} >
      <App />
  </Provider>
);

