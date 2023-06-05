import React, {JSX, RefObject, useEffect, useState} from 'react';
import {useInflowAppSelector} from "../store/Hooks";
import INavItemConfig from "./Nav/INavItemConfig";

import {Route, Routes} from "react-router-dom";
import Section from "./Section/Section";

import './Layout.scss';

export const LAYOUT_REF: RefObject<any> = React.createRef();

function Layout() {
    const [routes, setRoutes] = useState<JSX.Element[]>([]);

    const sectionsDisplayedInNavConfigPromise: Promise<[]> = useInflowAppSelector((state) => {
        return state.SectionsDisplayedInNavPromise.promiseValue;
    });

    useEffect(() => {
        sectionsDisplayedInNavConfigPromise
            .then((sectionsDisplayedInNavConfig) => {
                const routes = getRoutesBySectionDisplayedInNavConfig(sectionsDisplayedInNavConfig);
                setRoutes(routes);
            });
    }, []);

    function getRoutesBySectionDisplayedInNavConfig(sectionsDisplayedInNavConfig: []): JSX.Element[] {
        const routes = sectionsDisplayedInNavConfig.map((sectionsDisplayedInNavConfigItem: INavItemConfig) => {
            const sectionsDisplayedInNavConfigItemName = sectionsDisplayedInNavConfigItem.Name;
            return <Route
                        path={sectionsDisplayedInNavConfigItemName}
                        element={<Section testText={sectionsDisplayedInNavConfigItemName} />}
                   />
            });

        // TODO: Добавить явный тип sectionsDisplayedInNavConfig здесь, выше по методу и в сторе.
        // TODO: Добавить роут для "/", который ведёт на первый в списке навигационного меню раздел.
        return routes;
    }

    return (
        <div ref={LAYOUT_REF} className="layout">
            <Routes>
                {routes}
            </Routes>
        </div>
    );
}

export default Layout;