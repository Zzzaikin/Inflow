import React, {JSX, useEffect, useState} from 'react';
import './Nav.scss'
import {LAYOUT_REF} from "../Layout";
import NavItem from './NavItem';
import INavItemConfig from "./INavItemConfig";
import * as DataService from '../../services/DataService';

function Nav() {
    const navComponentClassNameText : string = "nav-component";
    const navComponentCollapsedClassNameText : string = "nav-component collapsed";

    const [toggleButtonText, setToggleButtonText] = useState("<");
    const [navComponentClassName, setNavComponentClassName] = useState("nav-component");

    /**
     * TODO: Add redux usage.
     */
    const [navItems, setNavItems] = useState<JSX.Element[]>([]);

    let navItemsConfig: INavItemConfig[];

    useEffect(() => {
        getSectionConfigs()
            .then((result) => {
                navItemsConfig = result.map((sectionConfig: INavItemConfig) => {
                    return {
                        ...sectionConfig,
                        selected: false
                    }
                });

                navItemsConfig[0].selected = true;

                let navItemsFromSectionConfigs = getNavItemsFromConfig(navItemsConfig);
                setNavItems(navItemsFromSectionConfigs);
            });
    }, []);

    async function getSectionConfigs() {
        const selectRequestConfig = {
            columnNames: [
                "Id",
                "Name",
                "Image"
            ],
            entityName: "SectionsDisplayedInNav"
        };

        const sectionConfigs = await DataService.selectAsync(selectRequestConfig);
        return sectionConfigs;
    }

    function getNavItemsFromConfig(config: INavItemConfig[]) : JSX.Element[] {
        const navItems: JSX.Element[] = config.map((configItem: INavItemConfig) => {
            return(
                <div
                    id={`${configItem.id}`}
                    className={configItem.selected ? "nav-item selected" : "nav-item"}
                    onClick={(e: any) => onNavItemClick(e)}
                    style={{backgroundImage: configItem.image}}
                >
                    <NavItem text={configItem.name} image={configItem.image} />
                </div>
            );
        });

        return navItems;
    }

    function onNavItemClick(e: any) {
        navItemsConfig.forEach((navItemConfig: INavItemConfig, i: number, arr: any[]) => {
            if (navItemConfig.selected) {
                arr[i].selected = false
            }

            let target = e.target;

            if (navItemConfig.id === target.id) {
                arr[i].selected = true;
                return;
            }

            if (navItemConfig.id === target.parentElement.id) {
                arr[i].selected = true;
            }
        });

        let updatedNavItems = getNavItemsFromConfig(navItemsConfig);

        /*
         * TODO: Очень плохая реализация. Перерисовываются все NavItem'ы, что в будущем аукнется, но сейчам нет времени.
         * Необходимо перерисовывать только выбранный NavItem и выбранный ранее.
         */
        setNavItems(updatedNavItems);
    }

    function onToggleButtonClick() {
        const navClassName : string =
            navComponentClassName === navComponentClassNameText
                ? navComponentCollapsedClassNameText
                : navComponentClassNameText;

        setNavComponentClassName(navClassName);

        LAYOUT_REF.current.classList.toggle("nav-collapsed");

        const toggleText : string = toggleButtonText === "<" ? ">" : "<";
        setToggleButtonText(toggleText);
    }

    return(
        <div className={navComponentClassName}>
            <div className="inflow-label-container">
                <div className="inflow-label">Inflow</div>
            </div>
            <div className="nav-items-container">
                {navItems}
            </div>
            <div className="nav-controls">
                <div className="user-profile" />
                <div
                    className="toggle-button"
                    onClick={() => onToggleButtonClick()}
                >{toggleButtonText}</div>
            </div>
        </div>
    );
}

export default Nav;