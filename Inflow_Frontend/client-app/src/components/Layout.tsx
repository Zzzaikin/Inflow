import React, {JSX, RefObject, useEffect, useState} from 'react';
import {useInflowAppSelector} from "../store/Hooks";

import {Route, Routes} from "react-router-dom";
import Section from "./Section/Section";

import './Layout.scss';

export const LAYOUT_REF: RefObject<any> = React.createRef();

function Layout() {
    const [routes, setRoutes] = useState<JSX.Element[]>([]);

    const sectionsConfigPromise: Promise<[]> = useInflowAppSelector((state) => {
        return state.SectionsConfigPromise.promiseValue;
    });

    useEffect(() => {
        sectionsConfigPromise
            .then((sectionsConfig: {Name: string}[]) => {
                const routes = getRoutesBySectionsConfig(sectionsConfig);
                setRoutes(routes);
            });
    }, []);

    function getRoutesBySectionsConfig(sectionsConfig: {Name: string}[]): JSX.Element[] {
        const routes = sectionsConfig.map((sectionsConfigItem: {Name: string}) => {
            const sectionsConfigItemName = sectionsConfigItem.Name;
            return <Route
                        path={sectionsConfigItemName}
                        element={<Section testText={sectionsConfigItemName} />}
                   />
            });

        const rootRoute =
            <Route path="/" element={<Section testText={sectionsConfig[0].Name} />}/>;

        routes.push(rootRoute);
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